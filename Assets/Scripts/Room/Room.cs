using System.Collections;
using UnityEngine;

public sealed class Room : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private RoomData _roomData = null;
    [SerializeField] private Transform _cameraPoint = null;
    [SerializeField] private Transform _spawnPoint = null;

    // -- PROPERTIES

    public Vector3 SpawnPosition => _spawnPoint.position;

    // -- METHODS

    private void EnterDoor( RoomDoor door )
    {
        Player.Instance.PlayerCamera.transform.position = new Vector3
        (
            door.DoorData.LinkedRoomData.Room._cameraPoint.position.x,
            door.DoorData.LinkedRoomData.Room._cameraPoint.position.y,
            Player.Instance.PlayerCamera.transform.position.z
        );

        Player.Instance.Teleport( door.DoorData.LinkedDoorData.Door.EntrancePosition );

        LoadNeighbourRooms();

        Player.Instance.PlayerInput.Unlock();
    }

    private IEnumerator EnterDoorRoutine( RoomDoor door )
    {
        while( !GameManager.Instance.IsSceneLoadedOrLoading( door.DoorData.LinkedRoomData.SceneName ) )
        {
            yield return null;
        }

        EnterDoor( door );
    }

    public void LoadNeighbourRooms()
    {
        foreach( var door_data in _roomData.Doors )
        {
            GameManager.Instance.LoadScene( door_data.LinkedRoomData.SceneName, out _ );
        }
    }

    private void DoorData_OnDoorEnter( RoomDoor door )
    {
        if( !GameManager.Instance.IsSceneLoadedOrLoading( door.DoorData.LinkedRoomData.SceneName ) )
        {
            StartCoroutine( EnterDoorRoutine( door ) );
        }
        else
        {
            EnterDoor( door );
        }
    }

    // -- UNITY

    private void Awake()
    {
        _roomData.Room = this;
    }

    private void Start()
    {
        foreach( var door_data in _roomData.Doors )
        {
            door_data.Door.OnDoorEnter += DoorData_OnDoorEnter;
        }
    }

    private void OnDestroy()
    {
        foreach( var door_data in _roomData.Doors )
        {
            door_data.Door.OnDoorEnter -= DoorData_OnDoorEnter;
        }
    }
}
