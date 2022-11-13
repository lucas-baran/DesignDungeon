using UnityEngine;
using UnityEngine.UI;

public enum EHeartDisplayType
{
    Empty,
    Half,
    Full,
    AllFull,
}

[System.Serializable]
public struct HeartDisplayData
{
    public EHeartDisplayType DisplayType;
    public Sprite FirstHalfSprite;
    public Sprite SecondHalfSprite;
}

public sealed class HeartDisplay : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private HeartDisplayData[] _heartDisplayDataList = null;
    [SerializeField] private Image _firstHalfImage = null;
    [SerializeField] private Image _secondHalfImage = null;

    // -- METHODS

    public void SetDisplay( EHeartDisplayType display_type )
    {
        foreach( var heart_display_data in _heartDisplayDataList )
        {
            if( display_type == heart_display_data.DisplayType )
            {
                _firstHalfImage.sprite = heart_display_data.FirstHalfSprite;
                _secondHalfImage.sprite = heart_display_data.SecondHalfSprite;
            }
        }
    }

    public void SetLastHalf( bool is_last )
    {
        _secondHalfImage.enabled = !is_last;
    }
}
