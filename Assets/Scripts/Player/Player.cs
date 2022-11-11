using UnityEngine;

public sealed class Player : MonoBehaviour
{
    // -- PROPERTIES

    public static Player Instance { get; private set; }

    public PlayerController PlayerController { get; private set; }
    public PlayerInput PlayerInput { get; private set; }

    // -- UNITY

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }
        else
        {
            Destroy( this );

            return;
        }

        PlayerController = GetComponent<PlayerController>();
        PlayerInput = GetComponent<PlayerInput>();
    }
}
