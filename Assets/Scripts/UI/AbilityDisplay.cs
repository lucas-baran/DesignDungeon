using UnityEngine;
using UnityEngine.UI;

public sealed class AbilityDisplay : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private Image _abilityImage = null;
    [SerializeField] private Image _fillImage = null;

    private EAbilityCategory _abilityCategory = EAbilityCategory.Weapon;

    // -- PROPERTIES

    public EAbilityCategory AbilityCategory
    {
        get => _abilityCategory;
        set
        {
            _abilityCategory = value;

            UpdateAbilitySprite( Player.Instance.Abilities[ value ] );
        }
    }

    // -- METHODS

    private void UpdateAbilitySprite( AbilityData new_ability )
    {
        if( new_ability.Category == AbilityCategory )
        {
            _abilityImage.sprite = new_ability.Sprite;
        }
    }

    // -- UNITY

    private void Start()
    {
        Player.Instance.Abilities.OnAbilityChanged += UpdateAbilitySprite;

        UpdateAbilitySprite( Player.Instance.Abilities[ AbilityCategory ] );
    }

    private void Update()
    {
        _fillImage.fillAmount = Player.Instance.Abilities.GetAbilityCooldown01( AbilityCategory );
    }

    private void OnDestroy()
    {
        Player.Instance.Abilities.OnAbilityChanged -= UpdateAbilitySprite;
    }
}
