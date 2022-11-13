using UnityEngine;

[CreateAssetMenu( fileName = "Fireball", menuName = "Skills/Fireball" )]
public sealed class Fireball : AbilityData
{
    // -- FIELDS

    [SerializeField] private float _damage = 20f;

    // -- METHODS

    public override bool CanActivate()
    {
        return true;
    }

    public override void Activate()
    {
        Debug.Log( "FIREBALL!" );
    }

    public override void End()
    {

    }
}
