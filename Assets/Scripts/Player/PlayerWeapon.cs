using UnityEngine;

public sealed class PlayerWeapon : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private WeaponData _startingWeaponData = null;

    private WeaponData _selectedWeapon = null;

    // -- PROPERTIES

    public WeaponData SelectedWeapon
    {
        get => _selectedWeapon;
        set
        {
            if( value == _selectedWeapon )
            {
                return;
            }

            if( _selectedWeapon != null )
            {
                _selectedWeapon.EffectiveAbility.OnAbilityActivated -= WeaponAbility_OnAbilityActivated;
            }

            _selectedWeapon = value;
            _selectedWeapon.EffectiveAbility.OnAbilityActivated += WeaponAbility_OnAbilityActivated;
            Player.Instance.Abilities[ EAbilityCategory.Weapon ] = _selectedWeapon.EffectiveAbility;

            OnWeaponChanged?.Invoke( _selectedWeapon );
        }
    }

    private void WeaponAbility_OnAbilityActivated( AbilityData ability_data )
    {
        Player.Instance.Renderer.PlayWeaponAnimation( _selectedWeapon.Animation );
        AudioManager.Instance.PlaySound( ESoundType.Attack );

    }

    // -- EVENTS

    public delegate void WeaponChangedHandler( WeaponData new_weapon_data );
    public event WeaponChangedHandler OnWeaponChanged;

    // -- UNITY

    private void Awake()
    {
        _selectedWeapon = _startingWeaponData;
        _selectedWeapon.EffectiveAbility.OnAbilityActivated += WeaponAbility_OnAbilityActivated;
    }

    private void Start()
    {
        Player.Instance.Abilities[ EAbilityCategory.Weapon ] = _selectedWeapon.EffectiveAbility;
    }

    private void OnDestroy()
    {
        _selectedWeapon.EffectiveAbility.OnAbilityActivated -= WeaponAbility_OnAbilityActivated;
    }
}
