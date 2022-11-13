using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private float _moveSpeed = 5f;

    private Rigidbody2D _rigidbody2D = null;

    // -- METHODS

    public void ResetVelocity()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void AddForce( Vector2 force, ForceMode2D mode = ForceMode2D.Force )
    {
        _rigidbody2D.AddForce( force, mode );
    }

    private void PlayerInput_OnInputLocked()
    {
        ResetVelocity();
    }

    private void Instance_OnPauseStateChanged( bool is_paused )
    {
        ResetVelocity();
    }

    // -- UNITY

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Player.Instance.Input.OnInputLocked += PlayerInput_OnInputLocked;
        GameManager.Instance.OnPauseStateChanged += Instance_OnPauseStateChanged;
    }

    private void Update()
    {
        if( Player.Instance.Input.InputLocked )
        {
            return;
        }

        _rigidbody2D.velocity = _moveSpeed * Player.Instance.Input.AxisInput;
    }

    private void OnDestroy()
    {
        Player.Instance.Input.OnInputLocked -= PlayerInput_OnInputLocked;
        GameManager.Instance.OnPauseStateChanged -= Instance_OnPauseStateChanged;
    }
}
