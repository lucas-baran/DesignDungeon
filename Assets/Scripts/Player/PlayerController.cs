using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private AbilityData _startingAbility = null;

    private PlayerInput _playerInput = null;
    private Rigidbody2D _rigidbody2D = null;

    private AbilityData _selectedAbility = null;
    private float _specialAbilityActiveTimer = 0f;
    private float _specialAbilityCooldownTimer = 0f;
    private bool _specialAbilityActivated = false;

    // -- PROPERTIES

    private bool CanSpecialAbility => _specialAbilityActiveTimer <= 0f && _specialAbilityCooldownTimer <= 0f && _selectedAbility.CanActivate();

    public AbilityData SelectedAbility
    {
        get => _selectedAbility;
        set
        {
            if( value == _selectedAbility )
            {
                return;
            }

            _selectedAbility = value;

            OnSpecialAbilityChanged?.Invoke( _selectedAbility );
        }
    }

    // -- EVENTS

    public delegate void SpecialAbilityChangedHandler( AbilityData new_skill );
    public event SpecialAbilityChangedHandler OnSpecialAbilityChanged;

    // -- METHODS

    public void AddForce( Vector2 force, ForceMode2D mode = ForceMode2D.Force )
    {
        _rigidbody2D.AddForce( force, mode );
    }

    private void PlayerInput_OnInputLocked()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    // -- UNITY

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _playerInput = Player.Instance.PlayerInput;

        SelectedAbility = _startingAbility;

        Player.Instance.PlayerInput.OnInputLocked += PlayerInput_OnInputLocked;
    }

    private void Update()
    {
        _specialAbilityActiveTimer -= Time.deltaTime;
        _specialAbilityCooldownTimer -= Time.deltaTime;

        if( _specialAbilityActivated && _specialAbilityActiveTimer <= 0 )
        {
            _specialAbilityActivated = false;
            _specialAbilityCooldownTimer = _selectedAbility.Cooldown;

            _selectedAbility.End();
        }

        if( _playerInput.InputLocked )
        {
            return;
        }

        _rigidbody2D.velocity = _moveSpeed * _playerInput.AxisInput;

        if( _playerInput.SpecialAbilityDown && CanSpecialAbility )
        {
            _specialAbilityActiveTimer = _selectedAbility.ActiveTime;
            _specialAbilityActivated = true;

            _selectedAbility.Activate();
        }
    }

    private void OnDestroy()
    {
        Player.Instance.PlayerInput.OnInputLocked -= PlayerInput_OnInputLocked;
    }
}
