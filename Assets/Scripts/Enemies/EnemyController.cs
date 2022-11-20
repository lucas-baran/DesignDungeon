using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    // -- CONSTS

    protected const int StimulusCount = 8;

    // -- FIELDS

    [SerializeField] protected float _acceleration = 0.1f;
    [SerializeField] protected float _maxSpeed = 5f;

    private Rigidbody2D _rigidbody2D = null;

    protected bool _hasMovementStimulus = true;

    /* Directions are clockwise starting from Vector.up at index 0
     * Stimulus are in the range 0 and 1
     */
    protected readonly float[] _movementStimulus = new float[ StimulusCount ];
    protected readonly Vector3[] _movementDirections = new Vector3[ StimulusCount ]
    {
        Vector3.up,
        (Vector3.up + Vector3.right).normalized,
        Vector3.right,
        (Vector3.right + Vector3.down).normalized,
        Vector3.down,
       (Vector3.down + Vector3.left).normalized,
        Vector3.left,
        (Vector3.left + Vector3.up).normalized,
    };

    // -- METHODS

    public void Teleport( Vector2 position )
    {
        _rigidbody2D.velocity = Vector2.zero;
        transform.position = position;
    }

    public void AddForce( Vector2 force, ForceMode2D force_mode )
    {
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.AddForce( force, force_mode );
    }

    protected abstract void UpdateMovementStimulus();
    protected abstract void Attack();

    private void Move()
    {
        if( !_hasMovementStimulus )
        {
            return;
        }

        Vector3 movement_direction = Vector3.zero;

        for( int stimulus_index = 0; stimulus_index < StimulusCount; stimulus_index++ )
        {
            movement_direction += _movementStimulus[ stimulus_index ] * _movementDirections[ stimulus_index ];
        }

        movement_direction /= StimulusCount;

        Vector2 target_velocity = _maxSpeed * movement_direction;
        _rigidbody2D.velocity = Vector2.MoveTowards( _rigidbody2D.velocity, target_velocity, _acceleration );
    }

    // -- UNITY

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Attack();
        UpdateMovementStimulus();
        Move();
    }
}
