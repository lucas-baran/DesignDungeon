using System.Collections.Generic;
using UnityEngine;

public sealed class HealthPanel : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private HeartDisplay _heartDisplayPrefab = null;

    private List<HeartDisplay> _heartDisplayList = new List<HeartDisplay>();

    // -- METHODS

    private void UpdateDisplayCount()
    {
        int target_display_count = Mathf.CeilToInt( Player.Instance.Life.MaxHealth / 2f );
        int display_index = 0;

        for( ; display_index < target_display_count; display_index++ )
        {
            if( display_index == _heartDisplayList.Count )
            {
                var heart_display = Instantiate( _heartDisplayPrefab, transform );
                _heartDisplayList.Add( heart_display );
            }

            _heartDisplayList[ display_index ].gameObject.SetActive( true );
            _heartDisplayList[ display_index ].SetLastHalf( false );
        }

        if( Player.Instance.Life.MaxHealth % 2 == 1 )
        {
            _heartDisplayList[ display_index - 1 ].SetLastHalf( true );
        }

        for( ; display_index < _heartDisplayList.Count; display_index++ )
        {
            _heartDisplayList[ display_index ].gameObject.SetActive( false );
        }
    }

    private void UpdateDisplay()
    {
        if( Player.Instance.Life.CurrentHealth == Player.Instance.Life.MaxHealth )
        {
            foreach( var heart_display in _heartDisplayList )
            {
                heart_display.SetDisplay( EHeartDisplayType.AllFull );
            }

            return;
        }

        int health_count = 2;

        foreach( var heart_display in _heartDisplayList )
        {
            EHeartDisplayType display_type = EHeartDisplayType.Empty;

            if( health_count <= Player.Instance.Life.CurrentHealth )
            {
                display_type = EHeartDisplayType.Full;
            }
            else if( health_count - 1 == Player.Instance.Life.CurrentHealth )
            {
                display_type = EHeartDisplayType.Half;
            }

            heart_display.SetDisplay( display_type );

            health_count += 2;
        }
    }

    private void PlayerLife_OnHealthChanged( PlayerLife entity_life, int health_change )
    {
        UpdateDisplay();
    }

    private void PlayerLife_OnMaxHealthChanged( PlayerLife entity_life, int max_health_change )
    {
        UpdateDisplayCount();
        UpdateDisplay();
    }

    // -- UNITY

    private void Start()
    {
        UpdateDisplayCount();
        UpdateDisplay();

        Player.Instance.Life.OnMaxHealthChanged += PlayerLife_OnMaxHealthChanged;
        Player.Instance.Life.OnHealthChanged += PlayerLife_OnHealthChanged;
    }

    private void OnDestroy()
    {
        Player.Instance.Life.OnMaxHealthChanged -= PlayerLife_OnMaxHealthChanged;
        Player.Instance.Life.OnHealthChanged -= PlayerLife_OnHealthChanged;
    }
}
