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
            door.Room._cameraPoint.position.x,
            door.Room._cameraPoint.position.y,
            Player.Instance.PlayerCamera.transform.position.z
        );

        Player.Instance.Teleport( door.EntrancePosition );

        LoadNeighbourRooms();

        Player.Instance.PlayerInput.Unlock();
    }

    private IEnumerator EnterDoorRoutine( DoorData door_data )
    {
        while( !door_data.Door.LinkedDoorData.IsSceneLoaded )
        {
            yield return null;
        }

        EnterDoor( door_data.Door.LinkedDoorData.Door );
    }

    public void LoadNeighbourRooms()
    {
        foreach( var door_data in _roomData.Doors )
        {
            GameManager.Instance.LoadScene( door_data.Door.LinkedDoorData.SceneName, out _ );
        }
    }

    private void DoorData_OnDoorEnter( DoorData door_data )
    {
        if( !door_data.Door.LinkedDoorData.IsSceneLoaded )
        {
            StartCoroutine( EnterDoorRoutine( door_data ) );
        }
        else
        {
            EnterDoor( door_data.Door.LinkedDoorData.Door );
        }
    }

    // -- UNITY

    private void Start()
    {
        _roomData.Room = this;

        foreach( var door_data in _roomData.Doors )
        {
            door_data.Door.Room = this;

            door_data.OnDoorEnter += DoorData_OnDoorEnter;
        }
    }

    private void OnDestroy()
    {
        foreach( var door_data in _roomData.Doors )
        {
            door_data.OnDoorEnter -= DoorData_OnDoorEnter;
        }
    }
}
