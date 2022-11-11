using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] protected bool _oneTimeTrigger = true;
    [SerializeField] protected DialogueData _dialogueToDisplay = null;

    private int _triggerCount = 0;

    // -- PROPERTIES

    protected bool CanTrigger => !_oneTimeTrigger || _triggerCount == 0;

    // -- METHODS

    protected void TriggerDialogue()
    {
        if( !CanTrigger )
        {
            return;
        }

        GameManager.Instance.Player.PlayerController.Lock();

        DialogueSystem.Instance.OnDialogueEnded += DialogueSystem_OnDialogueEnded;
        DialogueSystem.Instance.StartDialogue( _dialogueToDisplay );
        _triggerCount++;
    }

    private void DialogueSystem_OnDialogueEnded()
    {
        GameManager.Instance.Player.PlayerController.Unlock();

        DialogueSystem.Instance.OnDialogueEnded -= DialogueSystem_OnDialogueEnded;
    }
}
