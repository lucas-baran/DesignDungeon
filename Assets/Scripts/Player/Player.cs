using UnityEngine;

public sealed class Player : MonoBehaviour
{
    // -- FIELDS

    private Transform _transform = null;

    // -- PROPERTIES

    public static Player Instance { get; private set; }

    public Camera Camera { get; private set; }
    public PlayerInput Input { get; private set; }
    public PlayerController Controller { get; private set; }
    public PlayerAbilities Abilities { get; private set; }
    public PlayerLife Life { get; private set; }

    // -- METHODS

    public void Teleport( Vector3 position )
    {
        _transform.position = position;
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

        _transform = transform;

        Camera = Camera.main;
        Input = GetComponent<PlayerInput>();
        Controller = GetComponent<PlayerController>();
        Abilities = GetComponent<PlayerAbilities>();
        Life = GetComponent<PlayerLife>();
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
