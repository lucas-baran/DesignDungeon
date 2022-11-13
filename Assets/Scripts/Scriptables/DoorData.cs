using UnityEngine;

[CreateAssetMenu( fileName = "RoomX_EastDoor", menuName = "Rooms/Door" )]
public sealed class DoorData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private RoomData _linkedRoomData = null;
    [SerializeField] private int _selectedLinkedDoorDataIndex = 0;

    // -- PROPERTIES

    public RoomDoor Door { get; set; }
    public RoomData LinkedRoomData => _linkedRoomData;
    public DoorData LinkedDoorData => _linkedRoomData.Doors[ _selectedLinkedDoorDataIndex ];
}
