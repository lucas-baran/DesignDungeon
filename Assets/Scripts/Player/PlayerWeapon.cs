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

            _selectedWeapon = value;
            Player.Instance.Abilities[ EAbilityCategory.Weapon ] = _selectedWeapon.EffectiveAbility;

            OnWeaponChanged?.Invoke( _selectedWeapon );
        }
    }

    // -- EVENTS

    public delegate void WeaponChangedHandler( WeaponData new_weapon_data );
    public event WeaponChangedHandler OnWeaponChanged;

    // -- UNITY

    private void Awake()
    {
        _selectedWeapon = _startingWeaponData;
    }

    private void Start()
    {
        Player.Instance.Abilities[ EAbilityCategory.Weapon ] = _selectedWeapon.EffectiveAbility;
    }
}
