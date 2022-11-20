using UnityEngine;

public abstract class AbilityEffector : MonoBehaviour
{
    public virtual bool CanActivateEffect( AbilityData ability_data )
    {
        return true;
    }

    public abstract void ActivateEffect( AbilityData ability_data );

    public virtual void EndEffect( AbilityData ability_data )
    {

    }
}
