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

    // -- PROPERTIES

    public ReadOnlyCollection<SceneData> Scenes => new ReadOnlyCollection<SceneData>( _scenes );
}
