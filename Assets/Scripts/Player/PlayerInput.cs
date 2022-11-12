using UnityEngine;

public sealed class PlayerInput : MonoBehaviour
{
    // -- CONSTS

    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const string NextDialogueName = "NextDialogue";
    private const string ToggleSkillTreeName = "ToggleSkillTree";

    // -- FIELDS

    private int _lockCount = 0;

    // -- PROPERTIES

    public Vector2 AxisInput { get; private set; }
    public bool NextDialogueDown { get; private set; }
    public bool InputLocked => _lockCount > 0;

    // -- EVENTS

    public delegate void ButtonDownHandler();

    public event ButtonDownHandler OnNextDialogueButtonDown;
    public event ButtonDownHandler OnToggleSkillTreeButtonDown;

    // -- METHODS

    public void Lock()
    {
        _lockCount++;
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
    }
}
