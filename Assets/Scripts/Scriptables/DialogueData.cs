using UnityEngine;

[CreateAssetMenu( fileName = "diag_", menuName = "Dialogue/Dialogue" )]
public sealed class DialogueData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private DialogueEntryData[] _dialogueEntryDataList = null;

    // -- PROPERTIES

    public DialogueEntryData[] DialogueEntryDataList => _dialogueEntryDataList;
}
