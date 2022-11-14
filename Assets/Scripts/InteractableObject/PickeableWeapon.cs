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
        Player.Instance.Weapon.SelectedWeapon = _weaponData;
    }

    // -- UNITY

    private void Awake()
    {
        _weaponRenderer = GetComponent<SpriteRenderer>();
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
