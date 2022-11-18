using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private RoomData SpawnRoom = null;

    [Header( "Scenarios" )]
    [SerializeField] private Scenario EditorScenario = null;
    [SerializeField] private Scenario DevelopmentBuildScenario = null;
    [SerializeField] private Scenario ReleaseBuildScenario = null;

    private GameSettings _gameSettings = new GameSettings();

    private bool _isPaused = false;
    private List<int> _loadedAndLoadingScenes = new List<int>();

    // -- PROPERTIES

    public static GameManager Instance { get; private set; }

    public GameSettings GameSettings => _gameSettings;

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

    public bool IsSceneLoadedOrLoading( int build_index )
    {
        return _loadedAndLoadingScenes.Contains( build_index );
    }

    public bool LoadScene( int build_index, out AsyncOperation scene_load )
    {
        var scene = SceneManager.GetSceneByBuildIndex( build_index );

        if( _loadedAndLoadingScenes.Contains( build_index ) )
        {
            scene_load = null;

            return false;
        }

        if( scene.isLoaded )
        {
            if( !_loadedAndLoadingScenes.Contains( build_index ) )
            {
                _loadedAndLoadingScenes.Add( build_index );
            }

            scene_load = null;

            return false;
        }

        _loadedAndLoadingScenes.Add( build_index );

        scene_load = SceneManager.LoadSceneAsync( build_index, LoadSceneMode.Additive );

        return true;
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

            return;
        }

        for( int scene_index = 0; scene_index < SceneManager.sceneCount; scene_index++ )
        {
            _loadedAndLoadingScenes.Add( SceneManager.GetSceneAt( scene_index ).buildIndex );
        }
    }

    private IEnumerator Start()
    {
#if UNITY_EDITOR
        Scenario load_scenario = EditorScenario;
#elif DEVELOPMENT_BUILD
        Scenario load_scenario = DevelopmentBuildScenario;
#else
        Scenario load_scenario = ReleaseBuildScenario;
#endif

        foreach( var scene_data in load_scenario.Scenes )
        {
            if( LoadScene( scene_data.BuildIndex, out AsyncOperation scenario_scene_load_operation ) )
            {
                while( !scenario_scene_load_operation.isDone )
                {
                    yield return null;
                }
            }
        }

        if( LoadScene( SpawnRoom.SceneBuildIndex, out AsyncOperation spawn_room_scene_load_operation ) )
        {
            while( !spawn_room_scene_load_operation.isDone )
            {
                yield return null;
            }
        }

        Player.Instance.Camera.transform.position = SpawnRoom.Room.GetCameraPosition();
        Player.Instance.Teleport( SpawnRoom.Room.SpawnPosition );
        SpawnRoom.Room.LoadNeighbourRooms();
    }
}
