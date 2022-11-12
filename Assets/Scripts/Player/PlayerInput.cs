using UnityEngine;

public sealed class PlayerInput : MonoBehaviour
{
    // -- CONSTS

    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const string NextDialogueName = "NextDialogue";
    private const string ToggleSkillTreeName = "ToggleSkillTree";
    private const string PerformWeaponAbilityName = "PerformWeaponAbility";
    private const string PerformSpecialAbilityName = "PerformSpecialAbility";

    // -- FIELDS

    private int _lockCount = 0;

    // -- PROPERTIES

    public Vector2 MousePosition { get; private set; }
    public Vector2 AxisInput { get; private set; }
    public bool NextDialogueDown { get; private set; }
    public bool WeaponAbilityDown { get; private set; }
    public bool SpecialAbilityDown { get; private set; }
    public bool InputLocked => _lockCount > 0;

    // -- EVENTS

    public delegate void ButtonDownHandler();

    public event ButtonDownHandler OnInputLocked;
    public event ButtonDownHandler OnNextDialogueButtonDown;
    public event ButtonDownHandler OnToggleSkillTreeButtonDown;

    // -- METHODS

    public void Lock()
    {
        _lockCount++;

        NextDialogueDown = false;
        WeaponAbilityDown = false;
        SpecialAbilityDown = false;

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
        if( InputLocked )
        {
            return;
        }

        MousePosition = Player.Instance.PlayerCamera.ScreenToWorldPoint( Input.mousePosition );

        AxisInput = new Vector2
        (
            Input.GetAxisRaw( HorizontalAxisName ),
            Input.GetAxisRaw( VerticalAxisName )
        ).normalized;

        NextDialogueDown = Input.GetButtonDown( NextDialogueName );
        if( NextDialogueDown )
        {
            OnNextDialogueButtonDown?.Invoke();
        }

        if( Input.GetButtonDown( ToggleSkillTreeName ) )
        {
            OnToggleSkillTreeButtonDown?.Invoke();
        }

        WeaponAbilityDown = Input.GetButtonDown( PerformWeaponAbilityName );

        SpecialAbilityDown = Input.GetButtonDown( PerformSpecialAbilityName );
    }
}
