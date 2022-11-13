using UnityEngine;
using UnityEngine.UI;

public sealed class AbilityDisplay : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private EAbilityCategory AbilityCategory = EAbilityCategory.Normal;
    [SerializeField] private Image AbilityImage = null;
    [SerializeField] private Image FillImage = null;

    // -- METHODS

    private void PlayerController_OnSpecialAbilityChanged( AbilityData new_ability )
    {
        if( new_ability.Category == AbilityCategory )
        {
            AbilityImage.sprite = new_ability.Sprite;
        }
    }

    // -- UNITY

    private void Start()
    {
        Player.Instance.Abilities.OnAbilityChanged += PlayerController_OnSpecialAbilityChanged;

        PlayerController_OnSpecialAbilityChanged( Player.Instance.Abilities[ AbilityCategory ] );
    }

    private void Update()
    {
        FillImage.fillAmount = Player.Instance.Abilities.GetAbilityCooldown01( AbilityCategory );
    }

    private void OnDestroy()
    {
        Player.Instance.Abilities.OnAbilityChanged -= PlayerController_OnSpecialAbilityChanged;
    }
}
