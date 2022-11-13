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
        return Player.Instance.Input.AxisInput != Vector2.zero;
    }

    public override void Activate()
    {
        Vector2 dash_direction = Player.Instance.Input.AxisInput;

        Player.Instance.Input.Lock();

        if( _invincibility )
        {
            Player.Instance.Life.Invincible = true;
        }

        Player.Instance.Controller.AddForce( _dashVelocity * dash_direction, ForceMode2D.Impulse );
    }

    public override void End()
    {
        Player.Instance.Input.Unlock();

        if( _invincibility )
        {
            Player.Instance.Life.Invincible = false;
        }
    }
}
