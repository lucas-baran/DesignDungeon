using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private int SpawnScene = 0;

    private bool _isPaused = false;

    // -- PROPERTIES

    public static GameManager Instance { get; private set; }

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

    // -- METHODS

    public void LoadScene( string scene_name )
    {
        if( SceneManager.GetSceneByName(scene_name).isLoaded )
        {
            return;
        }

        SceneManager.LoadSceneAsync( scene_name, LoadSceneMode.Additive );
    }

    // -- UNITY

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;
        }
        else
        {
            Destroy( this );
        }
    }

    private void Start()
    {
        
    }
}
