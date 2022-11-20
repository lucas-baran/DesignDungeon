using UnityEngine;

public enum EWeaponAnimation
{
    SwordSlash,
}

public sealed class PlayerRenderer : MonoBehaviour
{
    // -- CONSTS

    private static readonly int SwordSlash = Animator.StringToHash( "SwordSlash" );

    // -- FIELDS

    [SerializeField] private SpriteRenderer _playerRenderer = null;
    [SerializeField] private SpriteRenderer _weaponRenderer = null;

    private Animator _playerAnimator = null;

    // -- METHODS

    public void PlayWeaponAnimation( EWeaponAnimation weapon_animation )
    {
        switch( weapon_animation )
        {
            case EWeaponAnimation.SwordSlash:
                _playerAnimator.SetTrigger( SwordSlash );
                break;

            default:
                throw new System.NotImplementedException();
        }
    }

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

    private void Awake()
    {
        _playerAnimator = GetComponent<Animator>();
    }

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
