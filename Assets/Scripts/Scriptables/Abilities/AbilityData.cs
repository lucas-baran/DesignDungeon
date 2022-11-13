using UnityEngine;

public enum EAbilityCategory
{
    Normal,
    Special,
    Movement,
}

public abstract class AbilityData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _name = "Skill";
    [SerializeField] private EAbilityCategory _category = EAbilityCategory.Normal;
    [SerializeField] private float _activeTime = 2f;
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private Sprite _sprite = null;

    // -- PROPERTIES

    public string Name => _name;
    public EAbilityCategory Category => _category;
    public float Cooldown => _cooldown;
    public float ActiveTime => _activeTime;
    public Sprite Sprite => _sprite;

    // -- METHODS

    public abstract bool CanActivate();
    public abstract void Activate();
    public abstract void End();
}
