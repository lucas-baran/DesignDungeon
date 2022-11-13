using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private RoomData SpawnRoom = null;

    private bool _isPaused = false;
    private List<string> _loadedAndLoadingScenes = new List<string>();

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

    private IEnumerator LoadSpawnRoom()
    {
        if( LoadScene( SpawnRoom.SceneName, out AsyncOperation scene_load ) )
        {
            while( !scene_load.isDone )
            {
                yield return null;
            }
        }

        Player.Instance.Teleport( SpawnRoom.Room.SpawnPosition );
        SpawnRoom.Room.LoadNeighbourRooms();
    }

    public bool IsSceneLoadedOrLoading( string scene_name )
    {
        return _loadedAndLoadingScenes.Contains( scene_name );
    }

    public bool LoadScene( string scene_name, out AsyncOperation scene_load )
    {
        var scene = SceneManager.GetSceneByName( scene_name );

        if( _loadedAndLoadingScenes.Contains( scene_name ) )
        {
            scene_load = null;

            return false;
        }

        if( scene.isLoaded )
        {
            if( !_loadedAndLoadingScenes.Contains( scene_name ) )
            {
                _loadedAndLoadingScenes.Add( scene_name );
            }

            scene_load = null;

            return false;
        }

        _loadedAndLoadingScenes.Add( scene_name );

        scene_load = SceneManager.LoadSceneAsync( scene_name, LoadSceneMode.Additive );

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
            _loadedAndLoadingScenes.Add( SceneManager.GetSceneAt( scene_index ).name );
        }
    }

    private void Start()
    {
        StartCoroutine( LoadSpawnRoom() );
    }
}
