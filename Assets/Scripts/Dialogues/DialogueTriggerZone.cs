using UnityEngine;

public sealed class DialogueTriggerZone : DialogueTrigger
{
    // -- UNITY

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.CompareTag( TagConstants.PlayerTag ) )
        {
            TriggerDialogue();
        }
    }
}
