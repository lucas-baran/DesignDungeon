using UnityEngine;

public sealed class PlayerRenderer : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private SpriteRenderer _canInteractRenderer = null;
    [SerializeField] private SpriteRenderer _weaponRenderer = null;

    // -- METHODS

    private void PlayerController_OnCanInteractStateChanged( bool can_interact )
    {
        _canInteractRenderer.enabled = can_interact;
    }

    private void Weapon_OnWeaponChanged( WeaponData new_weapon_data )
    {
        _weaponRenderer.sprite = new_weapon_data.Sprite;
    }

    // -- UNITY

    private void Awake()
    {
        _canInteractRenderer.enabled = false;
    }

    private void Start()
    {
        _weaponRenderer.sprite = Player.Instance.Weapon.SelectedWeapon.Sprite;

        Player.Instance.Controller.OnCanInteractStateChanged += PlayerController_OnCanInteractStateChanged;
        Player.Instance.Weapon.OnWeaponChanged += Weapon_OnWeaponChanged;
    }

    private void OnDestroy()
    {
        Player.Instance.Controller.OnCanInteractStateChanged -= PlayerController_OnCanInteractStateChanged;
        Player.Instance.Weapon.OnWeaponChanged -= Weapon_OnWeaponChanged;
    }
}
