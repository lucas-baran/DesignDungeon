using UnityEngine;

public sealed class PlayerInput : MonoBehaviour
{
    // -- CONSTS NON

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
        if( Input.GetKeyDown( InputSystem.Instance.GetKey(EKeyActionType.SkipDialogue ) ) )
        {
            OnNextDialogueDown?.Invoke();
        }

        if( InputLocked )
        {
            return;
        }

        MousePosition = Player.Instance.Camera.ScreenToWorldPoint( Input.mousePosition );
        MouseDirectionFromPlayer = (Player.Instance.Input.MousePosition - Player.Instance.Transform.position.ToVector2()).normalized;

        int move_left = Input.GetKey( InputSystem.Instance.GetKey( EKeyActionType.MoveLeft ) ) ? -1 : 0;
        int move_right = Input.GetKey( InputSystem.Instance.GetKey( EKeyActionType.MoveRight ) ) ? 1 : 0;
        int move_up = Input.GetKey( InputSystem.Instance.GetKey( EKeyActionType.MoveUp ) ) ? 1 : 0;
        int move_down = Input.GetKey( InputSystem.Instance.GetKey( EKeyActionType.MoveDown ) ) ? -1 : 0;
        AxisInput = new Vector2
        (
            move_right + move_left,
            move_up + move_down
        ).normalized;

        if( AxisInput != Vector2.zero )
        {
            LastNotNullAxisInput = AxisInput;
        }

        if( Input.GetKeyDown( InputSystem.Instance.GetKey( EKeyActionType.PickObject ) ) )
        {
            OnPickObjectDown?.Invoke();
        }

        if( Input.GetKeyDown( InputSystem.Instance.GetKey( EKeyActionType.WeaponAbility ) ) )
        {
            OnAbilityDown?.Invoke( EAbilityCategory.Weapon );
        }

        if( Input.GetKeyDown( InputSystem.Instance.GetKey( EKeyActionType.MovementAbility ) ) )
        {
            OnAbilityDown?.Invoke( EAbilityCategory.Movement );
        }

        /*if( Input.GetKeyDown( InputSystem.Instance.GetKey( EKeyActionType.SpecialAbility) ) )
        {
            OnAbilityDown?.Invoke( EAbilityCategory.Special );
        }*/

        if( Input.GetKeyDown( InputSystem.Instance.GetKey( EKeyActionType.PotionAbility ) ) )
        {
            OnAbilityDown?.Invoke( EAbilityCategory.Potion );
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnPauseStateChanged -= GameManager_OnPauseStateChanged;
    }
}
