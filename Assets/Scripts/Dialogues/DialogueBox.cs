using System.Collections;
using TMPro;
using UnityEngine;

public sealed class DialogueBox : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private TMP_Text _textComponent = null;

    // -- PROPERTIES

    public bool Display
    {
        get => gameObject.activeSelf;
        set => gameObject.SetActive( value );
    }

    // -- EVENTS

    public delegate void DialogueEntryFinishedHandler( DialogueBox dialogue_box );
    public event DialogueEntryFinishedHandler OnDialogeEntryFinished;

    // -- METHODS

    /// <summary>
    /// speed = character number per second
    /// </summary>
    public void StartDialogueEntry( DialogueEntryData dialogue_data )
    {
        _textComponent.text = string.Empty;

        StartCoroutine( WriteDialogueEntryRoutine( dialogue_data ) );
    }

    private IEnumerator WriteDialogueEntryRoutine( DialogueEntryData dialogue_data )
    {
        WaitForSeconds wait_for_character = new WaitForSeconds( 1f / dialogue_data.Speed );

        foreach( var character in dialogue_data.Text )
        {
            yield return wait_for_character;

            _textComponent.text += character;
        }

        OnDialogeEntryFinished?.Invoke( this );
    }
}