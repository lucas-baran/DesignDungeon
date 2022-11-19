using System.Collections.Generic;
using UnityEngine;

public enum EQuestStatus
{
    Failure = 0,
    Success = 1,
}

public class Quest
{
    // -- FIELDS

    private readonly QuestData _questData = null;

    private int _currentStepIndex = 0;

    // -- PROPERTIES

    public QuestData QuestData => _questData;

    // -- EVENTS

    public delegate void QuestEndedHandler( Quest quest, EQuestStatus quest_status );
    public event QuestEndedHandler OnQuestEnded;

    // -- CONSTRUCTORS

    public Quest( QuestData quest_data )
    {
        _questData = quest_data;
    }

    // -- METHODS

    private void StartCurrentStep()
    {
        var quest_step = _questData.Steps[ _currentStepIndex ];

        quest_step.OnStepEnded += CurrentQuestStep_OnStepEnded;
        quest_step.Start();
    }

    private void CurrentQuestStep_OnStepEnded( EQuestStatus quest_status )
    {
        if( quest_status  == EQuestStatus.Failure)
        {
            OnQuestEnded?.Invoke( this, EQuestStatus.Failure );

            return;
        }

        _questData.Steps[ _currentStepIndex ].OnStepEnded -= CurrentQuestStep_OnStepEnded;
        _currentStepIndex++;

        if( _currentStepIndex == _questData.Steps.Length )
        {
            OnQuestEnded?.Invoke( this, EQuestStatus.Success );

            return;
        }

        StartCurrentStep();
    }
}

public sealed class QuestManager : MonoBehaviour
{
    // -- FIELDS

    private List<Quest> _activeQuests = new List<Quest>();

    // -- PROPERTIES

    public static QuestManager Instance { get; private set; }

    // -- EVENTS

    public delegate void QuestEndedHandler( QuestData quest_data, EQuestStatus quest_status );
    public event QuestEndedHandler OnQuestEnded;
    
    // -- METHODS

    public void StartQuest( QuestData quest_data )
    {
        if( _activeQuests.Exists( ( quest ) => quest.QuestData == quest_data ) )
        {
            Debug.LogError( $"Quest {quest_data.Name} has already been started" );

            return;
        }

        var quest = new Quest( quest_data );
        _activeQuests.Add( quest );

        quest.OnQuestEnded += Quest_OnQuestEnded;
    }

    private void Quest_OnQuestEnded( Quest quest, EQuestStatus quest_status )
    {
        quest.OnQuestEnded -= Quest_OnQuestEnded;
        _activeQuests.Remove( quest );

        OnQuestEnded?.Invoke( quest.QuestData, quest_status );
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
    }
}
