using UnityEngine;

public sealed class PlayerRenderer : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private SpriteRenderer _playerRenderer = null;
    [SerializeField] private SpriteRenderer _weaponRenderer = null;

    // -- METHODS

    private void UpdateFacingDirection()
    {
        bool flip = Player.Instance.Input.MouseDirectionFromPlayer.x < 0;

        if( _playerRenderer.flipX != flip )
        {
            _playerRenderer.flipX = flip;
        }
    }

    private void Weapon_OnWeaponChanged( WeaponData new_weapon_data )
    {
        _weaponRenderer.sprite = new_weapon_data.Sprite;
    }

    // -- UNITY

    private void Start()
    {
        _weaponRenderer.sprite = Player.Instance.Weapon.SelectedWeapon.Sprite;

        Player.Instance.Weapon.OnWeaponChanged += Weapon_OnWeaponChanged;
    }

    private void Update()
    {
        UpdateFacingDirection();
    }

    private void OnDestroy()
    {
        Player.Instance.Weapon.OnWeaponChanged -= Weapon_OnWeaponChanged;
    }
}
