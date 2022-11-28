public enum EDashDirectionSetting
{
    Mouse,
    Movement,
    LastNotNullMovement,
}

public sealed class GameSettings
{
    // -- FIELDS

    private EDashDirectionSetting _dashDirection = EDashDirectionSetting.LastNotNullMovement;

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
