using UnityEngine;

public sealed class DashEffector : AbilityEffector
{
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
            EDashDirectionSetting.LastNotNullMovement => Player.Instance.Input.LastNotNullAxisInput,
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
