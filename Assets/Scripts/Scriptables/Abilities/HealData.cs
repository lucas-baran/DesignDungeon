using UnityEngine;

[CreateAssetMenu( fileName = "Heal", menuName = "Skills/Heal" )]
public sealed class HealData : AbilityData
{
    // -- FIELDS

    [SerializeField] private int _heal = 1;

    // -- METHODS

    protected override void Execute()
    {
        Player.Instance.Life.ChangeHealth( _heal );
    }
}
