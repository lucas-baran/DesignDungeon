using System.Collections;
using TMPro;
using UnityEngine;

public sealed class DialogueBox : HideOnPlay
{
    // -- FIELDS

    [SerializeField] private TMP_Text _textComponent = null;

    private bool _speedUpDialogue = false;

    // -- PROPERTIES

    public bool Display
    {
        get => gameObject.activeSelf;
        set => gameObject.SetActive( value );
    }

    // -- EVENTS

    public delegate void DialogueEntryEndedHandler( DialogueBox dialogue_box );
    public event DialogueEntryEndedHandler OnDialogeEntryEnded;

    // -- METHODS

    /// <summary>
    /// speed = character number per second
    /// </summary>
    public void StartDialogueEntry( DialogueEntryData dialogue_data )
    {
        _speedUpDialogue = false;
        _textComponent.text = string.Empty;

        StartCoroutine( WriteDialogueEntryRoutine( dialogue_data ) );
    }

    private IEnumerator WriteDialogueEntryRoutine( DialogueEntryData dialogue_data )
    {
        foreach( var character in dialogue_data.Text )
        {
            yield return new WaitForSecondsRealtime( 1f / dialogue_data.Speed );

            if( _speedUpDialogue )
            {
                _speedUpDialogue = false;
                _textComponent.text = dialogue_data.Text;

                break;
            }
            else
            {
                _textComponent.text += character;
            }
        }

        OnDialogeEntryEnded?.Invoke( this );
    }

    private void PlayerInput_OnNextDialogueButtonDown()
    {
        _speedUpDialogue = true;
    }

    // -- UNITY

    protected override void Start()
    {
        base.Start();

        Player.Instance.Input.OnNextDialogueButtonDown += PlayerInput_OnNextDialogueButtonDown;
    }

    private void OnDestroy()
    {
        Player.Instance.Input.OnNextDialogueButtonDown -= PlayerInput_OnNextDialogueButtonDown;
    }
}
