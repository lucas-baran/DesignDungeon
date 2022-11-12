using UnityEngine;

public sealed class Player : MonoBehaviour
{
    private Transform _transform = null;

    // -- PROPERTIES

    public static Player Instance { get; private set; }

    public Camera PlayerCamera { get; private set; }
    public PlayerController PlayerController { get; private set; }
    public PlayerInput PlayerInput { get; private set; }

    // -- METHODS

    public void Teleport( Vector3 position )
    {
        _transform.position = position;
    }

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

        _transform = transform;

        PlayerCamera = Camera.main;
        PlayerController = GetComponent<PlayerController>();
        PlayerInput = GetComponent<PlayerInput>();
    }
}
