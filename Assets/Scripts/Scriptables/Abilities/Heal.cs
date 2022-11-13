using UnityEngine;

[CreateAssetMenu( fileName = "Heal", menuName = "Skills/Heal" )]
public sealed class Heal : AbilityData
{
    // -- FIELDS

    [SerializeField] private int _heal = 1;

    // -- METHODS

    public override void Activate()
    {
        Player.Instance.Life.ChangeHealth( _heal );
    }
}
