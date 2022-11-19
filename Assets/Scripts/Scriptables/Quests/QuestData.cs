using UnityEngine;

[CreateAssetMenu( fileName = "Quest", menuName = "Quests/Quest" )]
public sealed class QuestData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _name = null;
    [SerializeField] private string _description = null;
    [SerializeField] private string _successDescription = null;
    [SerializeField] private string _failureDescription = null;
    [SerializeField] private NPCData _questGiver = null;
    [SerializeField] private QuestStepData[] _steps = null;
    [SerializeField] private QuestRewardData[] _successRewards = null;

    // -- PROPERTIES

    public string Name => _name;
    public string Description => _description;
    public NPCData QuestGiver => _questGiver;
    public QuestStepData[] Steps => _steps;

    // -- PROPERTIES

}
