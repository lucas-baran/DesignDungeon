using UnityEngine;

public sealed class PlayerInput : MonoBehaviour
{
    // -- CONSTS

    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const string NextDialogueName = "NextDialogue";
    private const string PerformWeaponAbilityName = "PerformWeaponAbility";
    private const string PerformMovementAbilityName = "PerformMovementAbility";
    private const string PerformSpecialAbilityName = "PerformSpecialAbility";
    private const string PerformPotionAbilityName = "PerformPotionAbility";
    private const string PickObjectName = "PickObject";

    // -- FIELDS

    private int _lockCount = 0;

    // -- PROPERTIES

    public Vector2 MousePosition { get; private set; }
    public Vector2 MouseDirectionFromPlayer { get; private set; }
    public Vector2 AxisInput { get; private set; }
    public Vector2 LastNotNullAxisInput { get; private set; } = Vector2.up;
    public bool InputLocked => _lockCount > 0;

    // -- EVENTS

    public delegate void InputLockedHandler();
    public delegate void ButtonDownHandler();
    public delegate void AbilityDownHandler( EAbilityCategory ability_category );

    public event InputLockedHandler OnInputLocked;

    public event ButtonDownHandler OnNextDialogueDown;
    public event ButtonDownHandler OnPickObjectDown;

    public event AbilityDownHandler OnAbilityDown;

    // -- METHODS

    public void Lock()
    {
        _lockCount++;

        if( _lockCount == 1 )
        {
            OnInputLocked?.Invoke();
        }
    }

    public void Unlock()
    {
        _lockCount = Mathf.Max( 0, _lockCount - 1 );
    }

    private void GameManager_OnPauseStateChanged( bool is_paused )
    {
        if( is_paused )
        {
            Lock();
        }
        else
        {
            Unlock();
        }
    }

    // -- UNITY

    private void Start()
    {
        GameManager.Instance.OnPauseStateChanged += GameManager_OnPauseStateChanged;
    }

    private void Update()
    {
        if( Input.GetButtonDown( NextDialogueName ) )
        {
            OnNextDialogueDown?.Invoke();
        }

        if( InputLocked )
        {
            return;
        }

        MousePosition = Player.Instance.Camera.ScreenToWorldPoint( Input.mousePosition );
        MouseDirectionFromPlayer = (Player.Instance.Input.MousePosition - Player.Instance.Transform.position.ToVector2()).normalized;

        AxisInput = new Vector2
        (
            Input.GetAxisRaw( HorizontalAxisName ),
            Input.GetAxisRaw( VerticalAxisName )
        ).normalized;

        if( AxisInput != Vector2.zero )
        {
            LastNotNullAxisInput = AxisInput;
        }

        if( Input.GetButtonDown( PickObjectName ) )
        {
            OnPickObjectDown?.Invoke();
        }

        if( Input.GetButtonDown( PerformWeaponAbilityName ) )
        {
            OnAbilityDown?.Invoke( EAbilityCategory.Weapon );
        }

        if( Input.GetButtonDown( PerformMovementAbilityName ) )
        {
            OnAbilityDown?.Invoke( EAbilityCategory.Movement );
        }

        if( Input.GetButtonDown( PerformSpecialAbilityName ) )
        {
            OnAbilityDown?.Invoke( EAbilityCategory.Special );
        }

        if( Input.GetButtonDown( PerformPotionAbilityName ) )
        {
            OnAbilityDown?.Invoke( EAbilityCategory.Potion );
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPauseStateChanged -= GameManager_OnPauseStateChanged;
    }
}
