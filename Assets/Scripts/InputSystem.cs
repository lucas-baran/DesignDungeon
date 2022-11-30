using System.Collections.Generic;
using UnityEngine;

public enum EKeyActionType
{
    MoveUp,
    MoveDown,
    MoveLeft,
    MoveRight,
    WeaponAbility,
    MovementAbility,
    SpecialAbility,
    PotionAbility,
    PickObject,
}

public class InputSystem : MonoBehaviour
{
    // -- FIELDS

    private KeyCode[] _validKeyCodes = null;

    private bool _listenForKey = false;
    private EKeyActionType _listenedActionType = EKeyActionType.MoveUp;
    private Dictionary<EKeyActionType, KeyCode> _keyActionMap = null;

    // -- EVENTS

    public delegate void ActionKeyChangedHandler( EKeyActionType key_action_type, KeyCode key_code );
    public event ActionKeyChangedHandler OnActionKeyChanged;
    
    // -- PROPERTIES

    public static InputSystem Instance { get; private set; }

    // -- METHODS

    public void ListenActionKey( EKeyActionType action_type )
    {
        _listenedActionType = action_type;
        _listenForKey = true;
    }

    private void CancelListenedActionKey()
    {
        _listenForKey = false;
    }

    // -- UPDATE

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }
        else
        {
            Destroy( this );
            return;
        }

        _validKeyCodes = (KeyCode[])System.Enum.GetValues( typeof( KeyCode ) );
    }

    private void Update()
    {
        if( !_listenForKey )
        {
            return;
        }

        if( Input.GetKey( KeyCode.Escape ) )
        {
            CancelListenedActionKey();

            return;
        }

        foreach( KeyCode key_code in _validKeyCodes )
        {
            if( Input.GetKey( key_code ) )
            {
                _keyActionMap[ _listenedActionType ] = key_code;
                _listenForKey = false;

                OnActionKeyChanged?.Invoke( _listenedActionType, KeyCode.Return );
            }
        }
    }
}
