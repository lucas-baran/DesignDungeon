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
    [SerializeField] private AbilityEffector _effectorPrefab = null;

    private AbilityEffector _effector = null;

    // -- PROPERTIES

    public string Name => _name;
    public string Description => _description;
    public EAbilityCategory Category => _category;
    public float Cooldown => _cooldown;
    public float ActiveTime => _activeTime;
    public Sprite Sprite => _sprite;

    protected AbilityEffector Effector
    {
        get
        {
            if( _effector == null )
            {
                _effector = Instantiate( _effectorPrefab, Player.Instance.Controller.WeaponPivot );
            }

            return _effector;
        }
    }

    // -- EVENTS

    public delegate void AbilityActivateHandler( AbilityData ability_data );
    public event AbilityActivateHandler OnAbilityActivated;

    // -- METHODS

    public bool CanActivate()
    {
        return Effector.CanActivateEffect( this );
    }

    public void Activate()
    {
        if( !CanActivate() )
        {
            return;
        }

        Effector.ActivateEffect( this );

        OnAbilityActivated?.Invoke( this );
    }

    public void End()
    {
        Effector.EndEffect( this );
    }
}
