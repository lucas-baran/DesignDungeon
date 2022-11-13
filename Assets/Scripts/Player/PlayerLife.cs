using UnityEngine;

public sealed class PlayerLife : MonoBehaviour
{
    // -- CONSTS

    private const int MinimumHealth = 1;

    // -- FIELDS

    [SerializeField] private int _maxHealth = 6;
    [SerializeField] private int _startingHealth = 6;

    private int _currentHealth = 0;
    private bool _invincible = false;
    private bool _died = false;

    // -- PROPERTIES

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public bool Invincible
    {
        get => _invincible;
        set
        {
            if( value == _invincible )
            {
                return;
            }

            _invincible = value;

            OnInvincibilityStateChanged?.Invoke( _invincible );
        }
    }

    // -- EVENTS

    public delegate void PlayerDiedHandler( PlayerLife entity_life );
    public event PlayerDiedHandler OnDied = null;

    public delegate void PlayerHealthChangedHandler( PlayerLife entity_life, int health_change );
    public event PlayerHealthChangedHandler OnHealthChanged = null;
    
    public delegate void PlayerMaxHealthChangedHandler( PlayerLife entity_life, int max_health_change );
    public event PlayerMaxHealthChangedHandler OnMaxHealthChanged = null;

    public delegate void InvincibilityStateChangedHandler( bool invincible );
    public event InvincibilityStateChangedHandler OnInvincibilityStateChanged = null;

    // -- METHODS

    public void ChangeHealth( int health_change )
    {
        if( Invincible || _died || health_change == 0 )
        {
            return;
        }

        _currentHealth = Mathf.Clamp( _currentHealth + health_change, 0, _maxHealth );

        OnHealthChanged?.Invoke( this, health_change );

        if( _currentHealth == 0 )
        {
            _died = true;

            OnDied?.Invoke( this );
        }
    }

    public void ChangeMaxHealth( int max_health_change )
    {
        if( _died || max_health_change == 0 )
        {
            return;
        }

        _maxHealth = Mathf.Max( 0, _maxHealth + max_health_change);

        if( _maxHealth == 0 )
        {
            _died = true;

            OnDied?.Invoke( this );
        }
        else
        {
            OnMaxHealthChanged?.Invoke( this, max_health_change );
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
