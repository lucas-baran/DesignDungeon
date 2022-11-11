using UnityEngine;

[CreateAssetMenu( fileName = "diag_", menuName = "Dialogue/Dialogue" )]
public sealed class DialogueData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private DialogueEntryData[] _dialogueEntryDataList = null;
    [SerializeField] private bool _pauseGameDuringDialogue = true;

    // -- PROPERTIES

    public DialogueEntryData[] DialogueEntryDataList => _dialogueEntryDataList;
    public bool PauseGameDuringDialogue => _pauseGameDuringDialogue;
}
