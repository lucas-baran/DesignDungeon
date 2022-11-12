using UnityEngine;

[CreateAssetMenu( fileName = "Weapon", menuName = "Weapons/Weapon" )]
public sealed class WeaponData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private Sprite _sprite = null;
    [SerializeField] private AbilityData _effectiveSkill = null;

    // -- PROPERTIES

    public Sprite Sprite => _sprite;
    public AbilityData EffectiveSkill => _effectiveSkill;
}
