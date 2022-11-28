using System.Collections.ObjectModel;
using UnityEngine;

[System.Serializable]
public struct SceneData
{
    // -- FIELDS

    [SerializeField] private int _buildIndex;

    // -- PROPERTIES

    public int BuildIndex => _buildIndex;
}

[CreateAssetMenu( fileName = "scenario_", menuName = "Scenario" )]
public sealed class Scenario : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private SceneData[] _scenes = null;
    [SerializeField] private Optional<RoomData> _spawnRoom = new Optional<RoomData>( true, null );

    // -- PROPERTIES

    public ReadOnlyCollection<SceneData> Scenes => new ReadOnlyCollection<SceneData>( _scenes );
    public Optional<RoomData> SpawnRoom => _spawnRoom;
}
