using UnityEngine;

[CreateAssetMenu( fileName = "SwordAttack", menuName = "Skills/Sword Attack" )]
public sealed class SwordAttackData : AbilityData
{
    // -- FIELDS

    [SerializeField] private float _damage = 5f;

    // -- METHODS

    public override bool CanActivate()
    {
        return true;
    }

    public override void Activate()
    {
        Debug.Log( "Sword attack!" );
    }

    public override void End()
    {

    }
}
