using UnityEngine;

public sealed class Player : MonoBehaviour
{
    // -- PROPERTIES

    public static Player Instance { get; private set; }

    public Transform Transform { get; private set; }
    public Camera Camera { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerController Controller { get; private set; }
    public PlayerRenderer Renderer { get; private set; }
    public PlayerAbilities Abilities { get; private set; }
    public PlayerLife Life { get; private set; }
    public PlayerWeapon Weapon { get; private set; }

    // -- METHODS

    public void Teleport( Vector3 position )
    {
        transform.position = position;
    }

    private void PlayerLife_OnDied( PlayerLife player_life )
    {
        Input.Lock();
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

        Transform = transform;
        Camera = Camera.main;
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<PlayerController>();
        Renderer = GetComponent<PlayerRenderer>();
        Abilities = GetComponent<PlayerAbilities>();
        Life = GetComponent<PlayerLife>();
        Weapon = GetComponent<PlayerWeapon>();
    }

    private void Start()
    {
        Life.OnDied += PlayerLife_OnDied;
    }

    private void OnDestroy()
    {
        Life.OnDied -= PlayerLife_OnDied;
    }
}
