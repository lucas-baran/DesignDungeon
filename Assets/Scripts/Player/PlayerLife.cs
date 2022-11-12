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

    // -- PROPERTIES

    public int CurrentHealth => _currentHealth;
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

    public delegate void PlayerDamagedHandler( PlayerLife entity_life );
    public event PlayerDamagedHandler OnPlayerDamaged = null;

    public delegate void InvincibilityStateChangedHandler( bool invincible );
    public event InvincibilityStateChangedHandler OnInvincibilityStateChanged = null;

    // -- METHODS

    public void ChangeHealth( int health_change )
    {
        if( Invincible )
        {
            return;
        }

        _currentHealth = Mathf.Clamp( _currentHealth + health_change, 0, _maxHealth );

        if( _currentHealth == 0f )
        {
            OnDied?.Invoke( this );
        }
        else
        {
            OnPlayerDamaged?.Invoke( this );
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
