using UnityEngine;

[CreateAssetMenu( fileName = "RoomX", menuName = "Rooms/Room" )]
public sealed class RoomData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private DoorData[] _doors = null;

    // -- PROPERTIES

    public Room Room { get; set; }
    public string SceneName => name;
    public DoorData[] Doors => _doors;
}
