using UnityEngine;

[CreateAssetMenu( fileName = "Enemy", menuName = "Enemy" )]
public sealed class EnemyData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _name = null;
    [SerializeField] private Enemy _prefab = null;

    // -- PROPERTIES

    public string Name => _name;
    public Enemy Prefab => _prefab;
}
