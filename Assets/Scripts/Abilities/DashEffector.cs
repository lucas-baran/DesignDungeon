using UnityEngine;

public sealed class DashEffector : AbilityEffector
{
    public override bool CanActivateEffect( AbilityData ability_data )
    {
        return Player.Instance.Input.AxisInput != Vector2.zero;
    }

    public override void ActivateEffect( AbilityData ability_data )
    {
        DashData dash_data = (DashData)ability_data;

        Player.Instance.Input.Lock();

        if( dash_data.Invincibility )
        {
            Player.Instance.Life.Invincible = true;
        }

        Vector2 dash_direction = GameManager.Instance.GameSettings.DashDirection switch
        {
            EDashDirectionSetting.Mouse => Player.Instance.Input.MouseDirectionFromPlayer,
            EDashDirectionSetting.Movement => Player.Instance.Input.AxisInput,
            _ => throw new System.NotImplementedException(),
        };

        Player.Instance.Controller.AddForce( dash_data.DashVelocity * dash_direction, ForceMode2D.Impulse );
    }

    public override void EndEffect( AbilityData ability_data )
    {
        DashData dash_data = (DashData)ability_data;

        Player.Instance.Input.Unlock();

        if( dash_data.Invincibility )
        {
            Player.Instance.Life.Invincible = false;
        }
    }
}
