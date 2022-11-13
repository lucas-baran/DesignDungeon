using UnityEngine;

public sealed class DebugCheatManager : MonoBehaviour
{
    // -- UNITY

    private void Update()
    {
        bool shift_modifier = Input.GetKey( KeyCode.LeftShift ) || Input.GetKey( KeyCode.RightShift );
        bool crtl_modifier = Input.GetKey( KeyCode.LeftControl ) || Input.GetKey( KeyCode.RightControl );
        bool alt_modifier = Input.GetKey( KeyCode.LeftAlt ) || Input.GetKey( KeyCode.RightAlt );

        if( Input.GetKeyDown( KeyCode.F1 ) )
        {
            Player.Instance.Life.ChangeHealth( -1 );
        }
        else if( Input.GetKeyDown( KeyCode.F2 ) )
        {
            Player.Instance.Life.ChangeHealth( 1 );
        }
        else if( Input.GetKeyDown( KeyCode.F3 ) )
        {
            Player.Instance.Life.ChangeHealth( int.MinValue );
        }
    }
}
