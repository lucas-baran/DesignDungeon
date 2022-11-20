using System.Collections.Generic;
using UnityEngine;

public class AbilityUsage
{
    // -- FIELDS

    private float _activeTimer = 0f;
    private float _cooldownTimer = 0f;
    private bool _isActive = false;

    // -- PROPERTIES

    public AbilityData AbilityData { get; set; }
    public bool IsActive => _isActive;
    public float AbilityCooldown01 => Mathf.Max( 0f, _cooldownTimer ) / AbilityData.Cooldown;
    private bool CanActivate => !IsActive && !Player.Instance.Input.InputLocked && _cooldownTimer <= 0f && AbilityData.CanActivate();

    // -- CONSTRUCTORS

    public AbilityUsage( AbilityData ability_data )
    {
        AbilityData = ability_data;
    }

    // -- METHODS

    public void Activate()
    {
        if( !CanActivate )
        {
            return;
        }

        _activeTimer = AbilityData.ActiveTime;
        _isActive = true;

        AbilityData.Activate();
    }

    public void Update( float delta_time )
    {
        _activeTimer -= delta_time;
        _cooldownTimer -= delta_time;

        if( _isActive && _activeTimer <= 0 )
        {
            _isActive = false;
            _cooldownTimer = AbilityData.Cooldown;

            AbilityData.End();
        }
    }
}

public sealed class PlayerAbilities : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private AbilityData[] _startingAbilities = null;

    private Dictionary<EAbilityCategory, AbilityUsage> _abilityUsageMap = new Dictionary<EAbilityCategory, AbilityUsage>();

    // -- PROPERTIES

    public AbilityData this[ EAbilityCategory category ]
    {
        get => _abilityUsageMap[ category ].AbilityData;
        set
        {
            if( value.Category != category )
            {
                Debug.LogError( $"Tried to assign ability {value.Name} to category {category} while ability category is {value.Category}" );

                return;
            }

            if( value == _abilityUsageMap[ category ].AbilityData )
            {
                return;
            }

            _abilityUsageMap[ category ].AbilityData = value;

            OnAbilityChanged?.Invoke( value );
        }
    }

    // -- EVENTS

    public delegate void AbilityChangedHandler( AbilityData new_ability );
    public event AbilityChangedHandler OnAbilityChanged;

    // -- METHODS

    public float GetAbilityCooldown01( EAbilityCategory category )
    {
        return _abilityUsageMap[ category ].AbilityCooldown01;
    }

    private void PlayerInput_OnAbilityDown( EAbilityCategory ability_category )
    {
        var ability_usage = _abilityUsageMap[ ability_category ];
        ability_usage.Activate();
    }

    // -- UNITY

    private void Awake()
    {
        foreach( var ability_data in _startingAbilities )
        {
            _abilityUsageMap.Add( ability_data.Category, new AbilityUsage( ability_data ) );
        }
    }

    private void Start()
    {
        Player.Instance.Input.OnAbilityDown += PlayerInput_OnAbilityDown;
    }

    private void Update()
    {
        foreach( var (_, ability_usage) in _abilityUsageMap )
        {
            ability_usage.Update( Time.deltaTime );
        }
    }

    private void OnDestroy()
    {
        Player.Instance.Input.OnAbilityDown -= PlayerInput_OnAbilityDown;
    }
}
