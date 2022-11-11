using UnityEngine;

public sealed class Player : MonoBehaviour
{
    // -- PROPERTIES

    public PlayerController PlayerController { get; private set; }
    public PlayerInput PlayerInput { get; private set; }

    // -- UNITY

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerInput = GetComponent<PlayerInput>();
    }
}
