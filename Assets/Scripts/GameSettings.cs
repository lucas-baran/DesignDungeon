public enum EDashDirectionSetting
{
    Mouse,
    Movement,
}

public sealed class GameSettings
{
    // -- FIELDS

    private EDashDirectionSetting _dashDirection = EDashDirectionSetting.Movement;

    // -- PROPERTIES

    public EDashDirectionSetting DashDirection
    {
        get => _dashDirection;
        set
        {
            if( value == _dashDirection )
            {
                return;
            }

            _dashDirection = value;
        }
    }
}
