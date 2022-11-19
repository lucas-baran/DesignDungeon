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

    private int _currentStepIndex = 0;

    // -- PROPERTIES

    public string Name => _name;
    public string Description => _description;
    public NPCData QuestGiver => _questGiver;

    // -- EVENTS

    public delegate void QuestEndedHandler( QuestData quest_data, EQuestStatus quest_status );
    public event QuestEndedHandler OnQuestEnded;

    // -- METHODS

    public void Start()
    {
        _currentStepIndex = 0;

        StartCurrentStep();
    }

    private void StartCurrentStep()
    {
        var quest_step = _steps[ _currentStepIndex ];

        quest_step.OnStepEnded += CurrentQuestStep_OnStepEnded;
        quest_step.Start();
    }

    private void CurrentQuestStep_OnStepEnded( EQuestStatus quest_status )
    {
        _steps[ _currentStepIndex ].OnStepEnded -= CurrentQuestStep_OnStepEnded;

        if( quest_status == EQuestStatus.Failure )
        {
            OnQuestEnded?.Invoke( this, EQuestStatus.Failure );

            return;
        }

        _currentStepIndex++;

        if( _currentStepIndex == _steps.Length )
        {
            OnQuestEnded?.Invoke( this, EQuestStatus.Success );

            return;
        }

        StartCurrentStep();
    }
}
