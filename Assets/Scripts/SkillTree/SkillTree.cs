using UnityEngine;

public sealed class SkillTree : HideOnPlay
{
    // -- PROPERTIES

    public bool Display
    {
        get => gameObject.activeSelf;
        private set => gameObject.SetActive( value );
    }

    // -- METHODS

    private void PlayerInput_OnToggleSkillTreeButtonDown()
    {
        Display = !Display;
    }

    // -- UNITY

    protected override void Start()
    {
        base.Start();

        Player.Instance.PlayerInput.OnToggleSkillTreeButtonDown += PlayerInput_OnToggleSkillTreeButtonDown;
    }

    private void OnDestroy()
    {
        Player.Instance.PlayerInput.OnToggleSkillTreeButtonDown -= PlayerInput_OnToggleSkillTreeButtonDown;
    }
}
