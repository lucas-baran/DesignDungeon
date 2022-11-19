using UnityEngine;

[CreateAssetMenu( fileName = "NPC_", menuName = "NPC" )]
public sealed class NPCData : ScriptableObject
{
    // -- FIELDS

    [SerializeField] private string _name = null;
    [SerializeField] private Sprite _fullSprite = null;

    // -- PROPERTIES

    public string Name => _name;
    public Sprite FullSprite => _fullSprite;
}
