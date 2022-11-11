using UnityEngine;

public sealed class PlayerInput : MonoBehaviour
{
    // -- CONSTS

    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const string NextDialogueName = "NextDialogue";
    private const string ToggleSkillTreeName = "ToggleSkillTree";

    // -- PROPERTIES

    public Vector2 AxisInput { get; private set; }
    public bool NextDialogueDown { get; private set; }

    // -- EVENTS

    public delegate void ButtonDownHandler();

    public event ButtonDownHandler OnNextDialogueButtonDown;
    public event ButtonDownHandler OnToggleSkillTreeButtonDown;

    // -- UNITY

    private void Update()
    {
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
