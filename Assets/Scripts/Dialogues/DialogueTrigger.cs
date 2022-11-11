using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private bool _oneTimeTrigger = true;
    [SerializeField] private DialogueData _dialogueToDisplay = null;

    private int _triggerCount = 0;

    // -- PROPERTIES

    private bool CanTrigger => !_oneTimeTrigger || _triggerCount == 0;

    // -- METHODS

    protected void TriggerDialogue()
    {
        if( !CanTrigger )
        {
            return;
        }

        _triggerCount++;

        DialogueSystem.Instance.StartDialogue( _dialogueToDisplay );
    }
}
