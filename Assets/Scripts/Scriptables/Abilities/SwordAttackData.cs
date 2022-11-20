using UnityEngine;

[CreateAssetMenu( fileName = "SwordAttack", menuName = "Skills/Sword Attack" )]
public sealed class SwordAttackData : AbilityData
{
    // -- FIELDS

    [SerializeField] private float _damage = 5f;

    // -- METHODS

    protected override void Execute()
    {
        Debug.Log( "Sword attack!" );
    }
}
