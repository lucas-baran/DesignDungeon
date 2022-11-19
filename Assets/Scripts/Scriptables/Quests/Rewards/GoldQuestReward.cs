using UnityEngine;

[CreateAssetMenu( fileName = "QuestStep", menuName = "Quests/Rewards/Gold" )]
public sealed class GoldQuestReward : QuestRewardData
{
    // -- FIELDS

    [SerializeField, Min( 1 )] private int _goldAmount = 10;

    // -- PROPERTIES

    // -- METHODS

    public override void Grant()
    {
        throw new System.NotImplementedException();
    }
}
