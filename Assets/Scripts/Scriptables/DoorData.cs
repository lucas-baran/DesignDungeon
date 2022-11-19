using UnityEngine;

[CreateAssetMenu( fileName = "RoomX_EastDoor", menuName = "Rooms/Door" )]
public sealed class DoorData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private RoomData _linkedRoomData = null;
    [SerializeField] private DoorData _linkedDoorData = null;

    // -- PROPERTIES

    public RoomDoor Door { get; set; }
    public RoomData LinkedRoomData => _linkedRoomData;
    public DoorData LinkedDoorData => _linkedDoorData;
}
