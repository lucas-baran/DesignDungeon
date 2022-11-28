using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    // -- FIELDS

    [Header( "Scenarios" )]
    [SerializeField] private Scenario EditorScenario = null;
    [SerializeField] private Scenario DevelopmentBuildScenario = null;
    [SerializeField] private Scenario ReleaseBuildScenario = null;

    private GameSettings _gameSettings = new GameSettings();

    private bool _isPaused = false;
    private HashSet<int> _loadedAndLoadingScenes = new HashSet<int>();
    private Vector3 _nextRoomPosition = Vector3.zero;
    private Stack<Vector3> _freeRoomPositionStack = new Stack<Vector3>();

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

    public void LoadScenario( Scenario scenario )
    {
        StartCoroutine( LoadScenarioRoutine( scenario ) );
    }

    private IEnumerator LoadScenarioRoutine( Scenario scenario )
    {
        if( _loadedAndLoadingScenes.Count > 0 )
        {
            yield return StartCoroutine( UnloadCurrentScenario() );
        }

        foreach( var scene_data in scenario.Scenes )
        {
            if( LoadScene( scene_data.BuildIndex, out AsyncOperation scenario_scene_load_operation ) )
            {
                while( !scenario_scene_load_operation.isDone )
                {
                    yield return null;
                }
            }
        }

        if( scenario.SpawnRoom.HasValue( out RoomData spawn_room_data ) )
        {
            if( LoadScene( spawn_room_data.SceneBuildIndex, out AsyncOperation spawn_room_scene_load_operation ) )
            {
                while( !spawn_room_scene_load_operation.isDone )
                {
                    yield return null;
                }
            }

            spawn_room_data.Room.TeleportPlayer();
        }
    }

    private IEnumerator UnloadCurrentScenario()
    {
        IsPaused = true;

        List<AsyncOperation> scene_unload_operations = new List<AsyncOperation>();
        var loaded_scenes = new HashSet<int>( _loadedAndLoadingScenes );

        foreach( var scene_build_index in loaded_scenes )
        {
            if( UnloadScene( scene_build_index, out AsyncOperation scenario_scene_unload_operation ) )
            {
                scene_unload_operations.Add( scenario_scene_unload_operation );
            }
        }

        bool all_unloaded;
        do
        {
            all_unloaded = true;

            for( int unload_operation_index = scene_unload_operations.Count - 1; unload_operation_index >= 0; unload_operation_index-- )
            {
                all_unloaded &= scene_unload_operations[ unload_operation_index ].isDone;

                if( !all_unloaded )
                {
                    yield return null;

                    break;
                }

                scene_unload_operations.RemoveAt( unload_operation_index );
            }
        } while( !all_unloaded );

        IsPaused = false;
    }

    public bool IsSceneLoadedOrLoading( int build_index )
    {
        return _loadedAndLoadingScenes.Contains( build_index );
    }

    public bool LoadScene( int build_index, out AsyncOperation scene_load_operation )
    {
        var scene = SceneManager.GetSceneByBuildIndex( build_index );

        if( _loadedAndLoadingScenes.Contains( build_index ) )
        {
            scene_load_operation = null;

            return false;
        }

        if( scene.isLoaded )
        {
            if( !_loadedAndLoadingScenes.Contains( build_index ) )
            {
                _loadedAndLoadingScenes.Add( build_index );
            }

            scene_load_operation = null;

            return false;
        }

        _loadedAndLoadingScenes.Add( build_index );

        scene_load_operation = SceneManager.LoadSceneAsync( build_index, LoadSceneMode.Additive );

        return true;
    }

    public bool UnloadScene( int build_index, out AsyncOperation scene_load_operation )
    {
        var scene = SceneManager.GetSceneByBuildIndex( build_index );

        if( !_loadedAndLoadingScenes.Contains( build_index ) )
        {
            scene_load_operation = null;

            return false;
        }

        if( !scene.isLoaded )
        {
            if( _loadedAndLoadingScenes.Contains( build_index ) )
            {
                _loadedAndLoadingScenes.Remove( build_index );
            }

            scene_load_operation = null;

            return false;
        }

        _loadedAndLoadingScenes.Remove( build_index );

        scene_load_operation = SceneManager.UnloadSceneAsync( build_index, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects );

        return true;
    }

    public Vector3 UseNextRoomPosition()
    {
        if( _freeRoomPositionStack.Count > 0 )
        {
            return _freeRoomPositionStack.Pop();
        }

        Vector3 room_position = _nextRoomPosition;
        _nextRoomPosition = room_position + (2f * Player.Instance.Camera.orthographicSize + 5f) * Vector3.up;

        return room_position;
    }

    public void SetRoomPositionUnused( Vector3 room_position )
    {
        _freeRoomPositionStack.Push( room_position );
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
    }

    private void Start()
    {
#if UNITY_EDITOR
        Scenario load_scenario = EditorScenario;
#elif DEVELOPMENT_BUILD
        Scenario load_scenario = DevelopmentBuildScenario;
#else
        Scenario load_scenario = ReleaseBuildScenario;
#endif

        LoadScenario( load_scenario );
    }
}
