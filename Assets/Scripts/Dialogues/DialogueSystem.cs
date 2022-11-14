using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DialogueSystem : MonoBehaviour
{
    // -- TYPES

    [System.Serializable]
    public struct DialogueBoxData
    {
        public EDialoguePosition Position;
        public DialogueBox DialogueBox;
    }

    // -- FIELDS

    [SerializeField] private DialogueBoxData[] _dialogueBoxes = null;

    private Dictionary<EDialoguePosition, DialogueBox> _dialogueBoxMap = new Dictionary<EDialoguePosition, DialogueBox>();

    private DialogueBox _displayedBox = null;
    private bool _currentEntryFinished = false;
    private bool _nextDialogueInputPressed = false;
    private WaitUntil _waitUntilCanGoToNextEntry = null;
    private Coroutine _writeDialogueCoroutine = null;

    // -- PROPERTIES

    public static DialogueSystem Instance { get; private set; }

    // -- EVENTS

    public delegate void DialogueEndedHandler();
    public event DialogueEndedHandler OnDialogueEnded;

    // -- METHODS

    public bool StartDialogue( DialogueData dialogue_data )
    {
        if( _writeDialogueCoroutine != null )
        {
            Debug.LogWarning( "Tried to start a dialogue while another is already running." );

            return false;
        }

        _writeDialogueCoroutine = StartCoroutine( WriteDialogueRoutine( dialogue_data ) );

        return true;
    }

    private IEnumerator WriteDialogueRoutine( DialogueData dialogue_data )
    {
        if( dialogue_data.PauseGameDuringDialogue )
        {
            GameManager.Instance.IsPaused = true;
        }

        foreach( var dialogue_entry in dialogue_data.DialogueEntryDataList )
        {
            _currentEntryFinished = false;
            _nextDialogueInputPressed = false;

            StartDialogueEntry( dialogue_entry );

            yield return _waitUntilCanGoToNextEntry;

            if( _displayedBox != null )
            {
                _displayedBox.Display = false;
            }
        }

        if( dialogue_data.PauseGameDuringDialogue )
        {
            GameManager.Instance.IsPaused = false;
        }

        _currentEntryFinished = false;
        _nextDialogueInputPressed = false;
        _writeDialogueCoroutine = null;

        OnDialogueEnded?.Invoke();
    }

    private void StartDialogueEntry( DialogueEntryData dialogue_data )
    {
        _displayedBox = _dialogueBoxMap[ dialogue_data.Position ];
        _displayedBox.Display = true;

        _displayedBox.StartDialogueEntry( dialogue_data );
    }

    private void DialogueBox_OnDialogeEntryFinished( DialogueBox dialogue_box )
    {
        _currentEntryFinished = true;
    }

    private void PlayerInput_OnNextDialogueButtonDown()
    {
        if( _currentEntryFinished )
        {
            _nextDialogueInputPressed = true;
        }
    }

    // -- UNITY

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

        foreach( var dialogue_box_data in _dialogueBoxes )
        {
            _dialogueBoxMap.Add( dialogue_box_data.Position, dialogue_box_data.DialogueBox );

            dialogue_box_data.DialogueBox.OnDialogeEntryEnded += DialogueBox_OnDialogeEntryFinished;
        }

        _waitUntilCanGoToNextEntry = new WaitUntil( () => _currentEntryFinished && _nextDialogueInputPressed );
    }

    private void Start()
    {
        Player.Instance.Input.OnNextDialogueDown += PlayerInput_OnNextDialogueButtonDown;
    }

    private void OnDestroy()
    {
        Player.Instance.Input.OnNextDialogueDown -= PlayerInput_OnNextDialogueButtonDown;

        foreach( var dialogue_box_data in _dialogueBoxes )
        {
            dialogue_box_data.DialogueBox.OnDialogeEntryEnded -= DialogueBox_OnDialogeEntryFinished;
        }
    }
}
