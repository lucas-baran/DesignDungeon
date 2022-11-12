using UnityEngine;

public sealed class PlayerLife : MonoBehaviour
{
    // -- CONSTS

    private const int MinimumHealth = 1;

    // -- FIELDS

    [SerializeField] private int _maxHealth = 6;
    [SerializeField] private int _startingHealth = 6;

    private int _currentHealth = 0;

    // -- PROPERTIES

    public int CurrentHealth => _currentHealth;

    // -- EVENTS

    public delegate void EntityDiedHandler( PlayerLife entity_life );
    public event EntityDiedHandler OnDied = null;

    // -- METHODS

    public void ChangeHealth( int health_change )
    {
        _currentHealth = Mathf.Clamp( _currentHealth + health_change, 0, _maxHealth );

        if( _currentHealth == 0f )
        {
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
