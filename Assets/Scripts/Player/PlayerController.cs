using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private float _moveSpeed = 5f;

    private PlayerInput _playerInput = null;
    private Rigidbody2D _rigidbody2D = null;

    // -- UNITY

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _playerInput = Player.Instance.PlayerInput;
    }

    private void Update()
    {
        if( _playerInput.InputLocked )
        {
            _rigidbody2D.velocity = Vector2.zero;

            return;
        }

        _rigidbody2D.velocity = _moveSpeed * _playerInput.AxisInput;
    }
}
