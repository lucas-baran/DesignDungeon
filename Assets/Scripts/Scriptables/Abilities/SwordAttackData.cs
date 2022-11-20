using UnityEngine;

[CreateAssetMenu( fileName = "SwordAttack", menuName = "Skills/Sword Attack" )]
public sealed class SwordAttackData : AbilityData
{
    // -- FIELDS

    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _knockback = 1f;

    // -- PROPERTIES

    public float Damage => _damage;
    public float Knockback => _knockback;
}
