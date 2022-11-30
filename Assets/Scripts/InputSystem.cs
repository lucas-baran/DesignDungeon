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
    SkipDialogue,
}

public class InputSystem : MonoBehaviour
{
    // -- FIELDS

    private KeyCode[] _validKeyCodes = null;

    private bool _listenForKey = false;
    private EKeyActionType _listenedActionType = EKeyActionType.MoveUp;
    private Dictionary<EKeyActionType, KeyCode> _keyActionMap = new Dictionary<EKeyActionType, KeyCode>()
    {
        { EKeyActionType.MoveUp, KeyCode.W },
        { EKeyActionType.MoveDown, KeyCode.S },
        { EKeyActionType.MoveLeft, KeyCode.A },
        { EKeyActionType.MoveRight, KeyCode.D },
        { EKeyActionType.WeaponAbility, KeyCode.Mouse0 },
        { EKeyActionType.MovementAbility, KeyCode.Mouse1 },
        { EKeyActionType.SpecialAbility, KeyCode.Space },
        { EKeyActionType.PotionAbility, KeyCode.Q },
        { EKeyActionType.PickObject, KeyCode.E },
        { EKeyActionType.SkipDialogue, KeyCode.F },
    }
    ;

    // -- EVENTS

    public delegate void ActionKeyChangedHandler( EKeyActionType key_action_type, KeyCode key_code );
    public event ActionKeyChangedHandler OnActionKeyChanged;
   
    public delegate void CancelKeyChangedHandler(  );
    public event CancelKeyChangedHandler OnCancelKeyChanged;
    
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
        OnCancelKeyChanged?.Invoke();
    }

    public KeyCode GetKey(EKeyActionType key_action)
    {
        return _keyActionMap[ key_action ];
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

                OnActionKeyChanged?.Invoke( _listenedActionType, key_code );
            }
        }
    }
}
