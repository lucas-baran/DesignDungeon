using UnityEngine;

public sealed class EnemyLife : MonoBehaviour
{
    // -- CONSTS

    private const float MinimumHealth = 0.1f;

    // -- FIELDS

    [SerializeField] private float _maxHealth = 10f;
    [SerializeField] private float _startingHealth = 10f;

    private float _currentHealth = 0f;
    private bool _died = false;
    private int _lastHealthUpdateFrame = -1;

    // -- PROPERTIES

    public float CurrentHealth => _currentHealth;

    // -- EVENTS

    public delegate void EnemyDiedHandler( EnemyLife entity_life );
    public event EnemyDiedHandler OnDied = null;

    // -- METHODS

    public void ChangeHealth( float health_change )
    {
        if( _died || health_change == 0f || _lastHealthUpdateFrame == Time.frameCount )
        {
            return;
        }

        _lastHealthUpdateFrame = Time.frameCount;

        _currentHealth = Mathf.Clamp( _currentHealth + health_change, 0f, _maxHealth );

        if( _currentHealth == 0f )
        {
            _died = true;
            OnDied?.Invoke( this );
        }
    }

    // -- UNITY

    private void Awake()
    {
        _currentHealth = _startingHealth;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _maxHealth = Mathf.Max( MinimumHealth, _maxHealth );
        _startingHealth = Mathf.Clamp( _startingHealth, MinimumHealth, _maxHealth );
    }
#endif
}
