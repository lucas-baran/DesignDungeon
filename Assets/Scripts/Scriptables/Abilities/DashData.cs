using UnityEngine;

[CreateAssetMenu( fileName = "Dash", menuName = "Skills/Dash" )]
public sealed class DashData : AbilityData
{
    // -- FIELDS

    [SerializeField] private float _dashVelocity = 50f;
    [SerializeField] private bool _invincibility = true;

    // -- METHODS

    public override bool CanActivate()
    {
        return Player.Instance.PlayerInput.AxisInput != Vector2.zero;
    }

    public override void Activate()
    {
        Vector2 dash_direction = Player.Instance.PlayerInput.AxisInput;

        Player.Instance.PlayerInput.Lock();

        if( _invincibility )
        {
            Player.Instance.PlayerLife.Invincible = true;
        }

        Player.Instance.PlayerController.AddForce( _dashVelocity * dash_direction, ForceMode2D.Impulse );
    }

    public override void End()
    {
        Player.Instance.PlayerInput.Unlock();

        if( _invincibility )
        {
            Player.Instance.PlayerLife.Invincible = false;
        }
    }
}
