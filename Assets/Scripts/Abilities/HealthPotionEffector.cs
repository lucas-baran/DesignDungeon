public sealed class HealthPotionEffector : AbilityEffector
{
    public override void ActivateEffect( AbilityData heal_data )
    {
        HealData health_potion_data = (HealData)heal_data;

        Player.Instance.Life.ChangeHealth( health_potion_data.Heal );
        AudioManager.Instance.PlaySound( ESoundType.Happy );
    }
}
