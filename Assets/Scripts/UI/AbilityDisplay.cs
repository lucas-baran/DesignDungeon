using UnityEngine;
using UnityEngine.UI;

public sealed class AbilityDisplay : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private Image AbilityImage = null;
    [SerializeField] private Image FillImage = null;

    // -- METHODS

    private void PlayerController_OnSpecialAbilityChanged( AbilityData new_ability )
    {
        AbilityImage.sprite = new_ability.Sprite;
    }

    // -- UNITY

    private void Start()
    {
        Player.Instance.PlayerController.OnSpecialAbilityChanged += PlayerController_OnSpecialAbilityChanged;

        PlayerController_OnSpecialAbilityChanged( Player.Instance.PlayerController.SpecialAbility );
    }

    private void Update()
    {
        FillImage.fillAmount = Player.Instance.PlayerController.SpecialAbilityCooldown01;
    }

    private void Destroy()
    {
        Player.Instance.PlayerController.OnSpecialAbilityChanged -= PlayerController_OnSpecialAbilityChanged;
    }
}
