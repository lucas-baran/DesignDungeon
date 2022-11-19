using UnityEngine;

public abstract class QuestStepData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _description = null;

    // -- PROPERTIES

    public string Description => _description;

    // -- EVENTS

    public delegate void StepEndedHandler( EQuestStatus quest_status );
    public event StepEndedHandler OnStepEnded;

    // -- METHODS

    public abstract void Start();
    protected abstract void Clear();

    protected void End( EQuestStatus quest_status )
    {
        Clear();

        OnStepEnded?.Invoke( quest_status );
    }
}
