using UnityEngine;

public sealed class PlayerInput : MonoBehaviour
{
    // -- CONSTS

    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const string NextDialogueName = "NextDialogue";
    private const string ToggleSkillTreeName = "ToggleSkillTree";
    private const string PerformNormalAbilityName = "PerformNormalAbility";
    private const string PerformMovementAbilityName = "PerformMovementAbility";
    private const string PerformSpecialAbilityName = "PerformSpecialAbility";
    private const string PerformPotionAbilityName = "PerformPotionAbility";

    // -- FIELDS

    private int _lockCount = 0;

    // -- PROPERTIES

    public Vector2 MousePosition { get; private set; }
    public Vector2 AxisInput { get; private set; }
    public bool NextDialogueDown { get; private set; }
    public bool WeaponAbilityDown { get; private set; }
    public bool InputLocked => _lockCount > 0;

    // -- EVENTS

    public delegate void ButtonDownHandler();

    public event ButtonDownHandler OnInputLocked;
    public event ButtonDownHandler OnNextDialogueButtonDown;
    public event ButtonDownHandler OnToggleSkillTreeButtonDown;

    public delegate void AbilityDownHandler( EAbilityCategory ability_category );
    public event AbilityDownHandler OnAbilityDown;

    // -- METHODS

    public void Lock()
    {
        _lockCount++;

        WeaponAbilityDown = false;

        if( _lockCount == 1 )
        {
            OnInputLocked?.Invoke();
        }
    }

    public void Unlock()
    {
        _lockCount = Mathf.Max( 0, _lockCount - 1 );
    }

    // -- UNITY

    private void Update()
    {
        NextDialogueDown = Input.GetButtonDown( NextDialogueName );
        if( NextDialogueDown )
        {
            OnNextDialogueButtonDown?.Invoke();
        }

        if( InputLocked )
        {
            return;
        }

        MousePosition = Player.Instance.Camera.ScreenToWorldPoint( Input.mousePosition );

        AxisInput = new Vector2
        (
            Input.GetAxisRaw( HorizontalAxisName ),
            Input.GetAxisRaw( VerticalAxisName )
        ).normalized;

        if( Input.GetButtonDown( ToggleSkillTreeName ) )
        {
            OnToggleSkillTreeButtonDown?.Invoke();
        }

        if( Input.GetButtonDown( PerformNormalAbilityName ) )
        {
            OnAbilityDown?.Invoke( EAbilityCategory.Normal );
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
}
