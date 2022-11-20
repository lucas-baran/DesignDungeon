using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Transform _weaponPivot = null;

    private Rigidbody2D _rigidbody2D = null;
    private List<Collider2D> _interactableObjectColliders = new List<Collider2D>();
    private IInteractableObject _closestInteractableObject = null;
    private bool _lockWeaponPivot = false;

    // -- PROPERTIES

    public Transform WeaponPivot => _weaponPivot;

    // -- EVENTS

    public delegate void CanInteractStateChangedHandler( bool can_interact );
    public event CanInteractStateChangedHandler OnCanInteractStateChanged;

    // -- METHODS

    public void ResetVelocity()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    public void AddForce( Vector2 force, ForceMode2D mode = ForceMode2D.Force )
    {
        _rigidbody2D.AddForce( force, mode );
    }

    private void UpdateClosestInteractable()
    {
        if( _interactableObjectColliders.Count == 0 )
        {
            if( _closestInteractableObject != null )
            {
                _closestInteractableObject.SetInteractable( false );
                _closestInteractableObject = null;
            }

            return;
        }

        Collider2D closest_collider = null;
        float closest_sqr_distance = float.MaxValue;

        foreach( var object_collider in _interactableObjectColliders )
        {
            float sqr_distance = Vector2.SqrMagnitude( transform.position - object_collider.transform.position );

            if( sqr_distance < closest_sqr_distance )
            {
                closest_sqr_distance = sqr_distance;
                closest_collider = object_collider;
            }
        }

        var closest_interactable = closest_collider.GetComponent<IInteractableObject>();

        if( closest_interactable == _closestInteractableObject )
        {
            return;
        }

        if( _closestInteractableObject != null )
        {
            _closestInteractableObject.SetInteractable( false );
        }

        _closestInteractableObject = closest_interactable;
        _closestInteractableObject.SetInteractable( true );
    }

    // Animation events
    public void LockWeaponPivot()
    {
        _lockWeaponPivot = true;
    }

    // Animation events
    public void UnlockWeaponPivot()
    {
        _lockWeaponPivot = false;
    }

    private void UpdateWeaponPivot()
    {
        if( _lockWeaponPivot )
        {
            return;
        }

        Vector3 weapon_pivot_rotation = Vector3.zero;
        Vector3 weapon_pivot_right_direction = Vector3.right;
        Vector3 weapon_pivot_forward_direction = Vector3.forward;
        float weapon_pivot_y_rotation = 0f;

        if( Player.Instance.Input.MouseDirectionFromPlayer.x < 0 )
        {
            weapon_pivot_y_rotation = 180f;
            weapon_pivot_right_direction = Vector3.left;
            weapon_pivot_forward_direction = Vector3.back;
        }

        weapon_pivot_rotation.y = weapon_pivot_y_rotation;
        weapon_pivot_rotation.z = Vector3.SignedAngle( weapon_pivot_right_direction, Player.Instance.Input.MouseDirectionFromPlayer, weapon_pivot_forward_direction );

        _weaponPivot.rotation = Quaternion.Euler( weapon_pivot_rotation );
    }

    private void PlayerInput_OnInputLocked()
    {
        ResetVelocity();
    }

    private void PlayerInput_OnPickObjectDown()
    {
        if( _interactableObjectColliders.Count == 0 )
        {
            return;
        }

        _closestInteractableObject.Interact();
    }

    private void GameManager_OnPauseStateChanged( bool is_paused )
    {
        ResetVelocity();
    }

    // -- UNITY

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Player.Instance.Input.OnInputLocked += PlayerInput_OnInputLocked;
        Player.Instance.Input.OnPickObjectDown += PlayerInput_OnPickObjectDown;
        GameManager.Instance.OnPauseStateChanged += GameManager_OnPauseStateChanged;
    }

    private void Update()
    {
        UpdateClosestInteractable();
        UpdateWeaponPivot();

        if( Player.Instance.Input.InputLocked )
        {
            return;
        }

        _rigidbody2D.velocity = _moveSpeed * Player.Instance.Input.AxisInput;
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.CompareTag( TagConstants.InteractableObjectTag ) )
        {
            _interactableObjectColliders.Add( collision );

            if( _interactableObjectColliders.Count == 1 )
            {
                OnCanInteractStateChanged?.Invoke( true );
            }
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        if( collision.CompareTag( TagConstants.InteractableObjectTag )
            && _interactableObjectColliders.Remove( collision )
            && _interactableObjectColliders.Count == 0
            )
        {
            OnCanInteractStateChanged?.Invoke( false );
        }
    }

    private void OnDestroy()
    {
        Player.Instance.Input.OnInputLocked -= PlayerInput_OnInputLocked;
        Player.Instance.Input.OnPickObjectDown -= PlayerInput_OnPickObjectDown;
        GameManager.Instance.OnPauseStateChanged -= GameManager_OnPauseStateChanged;
    }
}
