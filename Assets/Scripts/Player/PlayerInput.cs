using UnityEngine;

public sealed class PlayerInput : MonoBehaviour
{
    // -- CONSTS

    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";
    private const string NextDialogueName = "NextDialogue";

    // -- PROPERTIES

    public Vector2 AxisInput { get; private set; }
    public bool NextDialogue { get; private set; }
    
    // -- UNITY

    private void Update()
    {
        AxisInput = new Vector2
        (
            Input.GetAxisRaw( HorizontalAxisName ),
            Input.GetAxisRaw( VerticalAxisName )
        ).normalized;

        NextDialogue = Input.GetButton( NextDialogueName );
    }
}
