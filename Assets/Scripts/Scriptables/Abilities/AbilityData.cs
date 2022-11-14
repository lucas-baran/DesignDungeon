using UnityEngine;

public enum EAbilityCategory
{
    Weapon,
    Special,
    Movement,
    Potion,
}

public abstract class AbilityData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _name = "Skill";
    [SerializeField, Multiline] private string _description = null;
    [SerializeField] private EAbilityCategory _category = EAbilityCategory.Weapon;
    [SerializeField] private float _activeTime = 2f;
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private Sprite _sprite = null;

    // -- PROPERTIES

    public string Name => _name;
    public string Description => _description;
    public EAbilityCategory Category => _category;
    public float Cooldown => _cooldown;
    public float ActiveTime => _activeTime;
    public Sprite Sprite => _sprite;

    // -- METHODS

    public virtual bool CanActivate()
    {
        return true;
    }

    public abstract void Activate();

    public virtual void End()
    {

    }
}
