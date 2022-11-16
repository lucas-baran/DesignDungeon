using UnityEngine;

public sealed class PickeableWeapon : MonoBehaviour, IInteractableObject
{
    // -- FIELDS

    [SerializeField] private SpriteRenderer _weaponRenderer = null;
    [SerializeField] private WeaponData _weaponData = null;

    // -- PROPERTIES

    public Transform Transform { get; private set; }

    public WeaponData WeaponData
    {
        set
        {
            _weaponData = value;
            _weaponRenderer.sprite = _weaponData.Sprite;
        }
    }

    // -- METHODS

    public void Interact()
    {
        var player_weapon = Player.Instance.Weapon.SelectedWeapon;
        Player.Instance.Weapon.SelectedWeapon = _weaponData;
        WeaponData = player_weapon;
    }

    // -- UNITY

    private void Awake()
    {
        Transform = transform;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if( _weaponRenderer != null && _weaponData != null )
        {
            _weaponRenderer.sprite = _weaponData.Sprite;
        }
    }
#endif
}
