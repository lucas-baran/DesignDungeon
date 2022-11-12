using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _name = "Skill";
    [SerializeField] private float _activeTime = 2f;
    [SerializeField] private float _cooldown = 2f;
    [SerializeField] private Sprite _sprite = null;

    // -- PROPERTIES

    public string Name => _name;
    public float Cooldown => _cooldown;
    public float ActiveTime => _activeTime;
    public Sprite Sprite => _sprite;

    // -- METHODS

    public abstract bool CanActivate();
    public abstract void Activate();
    public abstract void End();
}
