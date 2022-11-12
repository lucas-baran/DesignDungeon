using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private AbilityData _startingSpecialAbility = null;

    private PlayerInput _playerInput = null;
    private Rigidbody2D _rigidbody2D = null;

    private AbilityData _specialAbility = null;
    private float _specialAbilityActiveTimer = 0f;
    private float _specialAbilityCooldownTimer = 0f;
    private bool _specialAbilityActivated = false;

    // -- PROPERTIES

    public float SpecialAbilityCooldown01 => Mathf.Max( 0f, _specialAbilityCooldownTimer ) / _specialAbility.Cooldown;

    private bool CanSpecialAbility => _specialAbilityActiveTimer <= 0f && _specialAbilityCooldownTimer <= 0f && _specialAbility.CanActivate();

    public AbilityData SpecialAbility
    {
        get => _specialAbility;
        set
        {
            if( value == _specialAbility )
            {
                return;
            }

            _specialAbility = value;

            OnSpecialAbilityChanged?.Invoke( _specialAbility );
        }
    }

    // -- EVENTS

    public delegate void SpecialAbilityChangedHandler( AbilityData new_ability );
    public event SpecialAbilityChangedHandler OnSpecialAbilityChanged;

    // -- METHODS

    public void ResetVelocity()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void AddForce( Vector2 force, ForceMode2D mode = ForceMode2D.Force )
    {
        _rigidbody2D.AddForce( force, mode );
    }

    private void PlayerInput_OnInputLocked()
    {
        ResetVelocity();
    }

    private void Instance_OnPauseStateChanged( bool is_paused )
    {
        ResetVelocity();
    }

    // -- UNITY

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        SpecialAbility = _startingSpecialAbility;
    }

    private void Start()
    {
        _playerInput = Player.Instance.PlayerInput;

        Player.Instance.PlayerInput.OnInputLocked += PlayerInput_OnInputLocked;
        GameManager.Instance.OnPauseStateChanged += Instance_OnPauseStateChanged;
    }

    private void Update()
    {
        _specialAbilityActiveTimer -= Time.deltaTime;
        _specialAbilityCooldownTimer -= Time.deltaTime;

        if( _specialAbilityActivated && _specialAbilityActiveTimer <= 0 )
        {
            _specialAbilityActivated = false;
            _specialAbilityCooldownTimer = _specialAbility.Cooldown;

            _specialAbility.End();
        }

        if( _playerInput.InputLocked )
        {
            return;
        }

        _rigidbody2D.velocity = _moveSpeed * _playerInput.AxisInput;

        if( _playerInput.SpecialAbilityDown && CanSpecialAbility )
        {
            _specialAbilityActiveTimer = _specialAbility.ActiveTime;
            _specialAbilityActivated = true;

            _specialAbility.Activate();
        }
    }

    private void OnDestroy()
    {
        Player.Instance.PlayerInput.OnInputLocked -= PlayerInput_OnInputLocked;
        GameManager.Instance.OnPauseStateChanged -= Instance_OnPauseStateChanged;
    }
}
