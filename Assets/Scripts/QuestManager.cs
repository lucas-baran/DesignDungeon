using System.Collections.Generic;
using UnityEngine;

public enum EQuestStatus
{
    Failure = 0,
    Success = 1,
}

public sealed class QuestManager : MonoBehaviour
{
    // -- FIELDS

    private List<QuestData> _finishedQuests = new List<QuestData>();
    private List<QuestData> _activeQuests = new List<QuestData>();

    // -- PROPERTIES

    public static QuestManager Instance { get; private set; }

    // -- EVENTS

    public delegate void QuestEndedHandler( QuestData quest_data, EQuestStatus quest_status );
    public event QuestEndedHandler OnQuestEnded;

    // -- METHODS

    public void StartQuest( QuestData quest_data )
    {
        if( _activeQuests.Contains( quest_data ) || _finishedQuests.Contains( quest_data ) )
        {
            Debug.LogError( $"Quest {quest_data.Name} has already been started" );

            return;
        }

        _activeQuests.Add( quest_data );

        quest_data.OnQuestEnded += Quest_OnQuestEnded;
        quest_data.Start();
    }

    private void Quest_OnQuestEnded( QuestData quest_data, EQuestStatus quest_status )
    {
        quest_data.OnQuestEnded -= Quest_OnQuestEnded;
        _activeQuests.Remove( quest_data );
        _finishedQuests.Add( quest_data );

        OnQuestEnded?.Invoke( quest_data, quest_status );
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
