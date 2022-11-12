using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu( fileName = "RoomX_EastDoor", menuName = "Rooms/Door" )]
public sealed class DoorData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _scene = "Room";

    // -- PROPERTIES

    public string SceneName => _scene;

    public RoomDoor Door { get; set; }
    public bool IsSceneLoaded => SceneManager.GetSceneByName( SceneName ).isLoaded;

    // -- EVENTS

    public delegate void DoorEnteredHandler( DoorData door_data );
    public event DoorEnteredHandler OnDoorEnter = null;

    // -- METHODS

    public void Enter()
    {
        OnDoorEnter?.Invoke( this );
    }
}
