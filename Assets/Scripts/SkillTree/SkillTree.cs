using UnityEngine;

public sealed class SkillTree : HideOnPlay
{
    // -- PROPERTIES

    public bool Display
    {
        get => gameObject.activeSelf;
        private set => gameObject.SetActive( value );
    }
}
