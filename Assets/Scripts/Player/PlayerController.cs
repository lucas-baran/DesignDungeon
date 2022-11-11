using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private float _moveSpeed = 5f;

    private PlayerInput _playerInput = null;
    private Rigidbody2D _rigidbody2D = null;

    private int _lockCount = 0;

    // -- PROPERTIES

    public bool LockPosition => _lockCount > 0;

    // -- METHODS

    public void Lock()
    {
        _rigidbody2D.velocity = Vector2.zero;

        _lockCount++;
    }
    
    public void Unlock()
    {
        _lockCount--;
    }

    // -- UNITY

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _playerInput = GameManager.Instance.Player.PlayerInput;
    }

    private void Update()
    {
        if( LockPosition )
        {
            return;
        }

        _rigidbody2D.velocity = _moveSpeed * _playerInput.AxisInput;
    }
}
