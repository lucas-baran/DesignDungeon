using UnityEngine;

public abstract class AbilityEffector : MonoBehaviour
{
    public abstract bool CanActivateEffect( AbilityData ability_data );
    public abstract void ActivateEffect( AbilityData ability_data );
    public abstract void EndEffect( AbilityData ability_data );
}
