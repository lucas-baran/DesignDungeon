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
    public Vector3 RoomCenter => transform.position;

    // -- METHODS

    public Vector3 GetCameraPosition()
    {
        return new Vector3
        (
            _cameraPoint.position.x,
            _cameraPoint.position.y,
            Player.Instance.Camera.transform.position.z
        );
    }

    public void TeleportPlayer()
    {
        Player.Instance.Camera.transform.position = GetCameraPosition();
        Player.Instance.Teleport( SpawnPosition );
        LoadNeighbourRooms();
        Player.Instance.CurrentRoom = this;
    }

    private void EnterDoor( RoomDoor door )
    {
        Player.Instance.Camera.transform.position = door.DoorData.LinkedRoomData.Room.GetCameraPosition();
        Player.Instance.Teleport( door.DoorData.LinkedDoorData.Door.EntrancePosition );

        LoadNeighbourRooms();

        Player.Instance.Input.Unlock();
        Player.Instance.CurrentRoom = this;
    }

    private IEnumerator EnterDoorRoutine( RoomDoor door )
    {
        while( !GameManager.Instance.IsSceneLoadedOrLoading( door.DoorData.LinkedRoomData.SceneBuildIndex ) )
        {
            yield return null;
        }

        EnterDoor( door );
    }

    public void LoadNeighbourRooms()
    {
        foreach( var door_data in _roomData.Doors )
        {
            GameManager.Instance.LoadScene( door_data.LinkedRoomData.SceneBuildIndex, out _ );
        }
    }

    private void DoorData_OnDoorEnter( RoomDoor door )
    {
        if( !GameManager.Instance.IsSceneLoadedOrLoading( door.DoorData.LinkedRoomData.SceneBuildIndex ) )
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
        transform.position = GameManager.Instance.UseNextRoomPosition();

        foreach( var door_data in _roomData.Doors )
        {
            door_data.Door.OnDoorEnter += DoorData_OnDoorEnter;
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.SetRoomPositionUnused( transform.position );

        foreach( var door_data in _roomData.Doors )
        {
            door_data.Door.OnDoorEnter -= DoorData_OnDoorEnter;
        }
    }
}
