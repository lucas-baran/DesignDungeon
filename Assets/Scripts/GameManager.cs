using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    // -- FIELDS

    private bool _isPaused = false;

    // -- PROPERTIES

    public static GameManager Instance { get; private set; }

    public Player Player { get; private set; }

    public bool IsPaused
    {
        get => _isPaused;
        set
        {
            if( _isPaused == value )
            {
                return;
            }

            _isPaused = value;

            if( _isPaused )
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }

            OnPauseStateChanged?.Invoke( _isPaused );
        }
    }

    // --EVENTS

    public delegate void PauseStateChanged( bool is_paused );
    public event PauseStateChanged OnPauseStateChanged;

    // -- UNITY

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;

            Player = FindObjectOfType<Player>();
        }
        else
        {
            Destroy( this );
        }
    }
}
