using UnityEngine;

[CreateAssetMenu( fileName = "Weapon", menuName = "Weapons/Weapon" )]
public sealed class WeaponData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _name = "Skill";
    [SerializeField, Multiline] private string _description = null;
    [SerializeField] private Sprite _sprite = null;
    [SerializeField] private AbilityData _effectiveSkill = null;

    // -- PROPERTIES

    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public AbilityData EffectiveSkill => _effectiveSkill;
}
