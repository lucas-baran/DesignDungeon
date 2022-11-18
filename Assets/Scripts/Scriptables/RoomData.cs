using UnityEngine;

[CreateAssetMenu( fileName = "RoomX", menuName = "Rooms/Room" )]
public sealed class RoomData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private SceneData _scene = default;
    [SerializeField, NonReorderable] private DoorData[] _doors = null;

    // -- PROPERTIES

    public Room Room { get; set; }
    public int SceneBuildIndex => _scene.BuildIndex;
    public DoorData[] Doors => _doors;
}
