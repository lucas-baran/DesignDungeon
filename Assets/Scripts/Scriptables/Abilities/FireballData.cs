using UnityEngine;

[CreateAssetMenu( fileName = "Fireball", menuName = "Skills/Fireball" )]
public sealed class FireballData : AbilityData
{
    // -- FIELDS

    [SerializeField] private float _damage = 20f;
}
