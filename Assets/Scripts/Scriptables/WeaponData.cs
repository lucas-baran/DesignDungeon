using UnityEngine;

[CreateAssetMenu( fileName = "Weapon", menuName = "Weapons/Weapon" )]
public sealed class WeaponData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _name = "Weapon";
    [SerializeField, Multiline] private string _description = null;
    [SerializeField] private Sprite _sprite = null;
    [SerializeField] private AbilityData _effectiveAbility = null;
    [SerializeField] private EWeaponAnimation _animation = EWeaponAnimation.SwordSlash;

    // -- PROPERTIES

    public string Name => _name;
    public string Description => _description;
    public Sprite Sprite => _sprite;
    public AbilityData EffectiveAbility => _effectiveAbility;
    public EWeaponAnimation Animation => _animation;
}
