using UnityEngine;

public enum EDialoguePosition
{
    BottomLeft,
    BottomRight,
    BottomCenter,
}

[CreateAssetMenu( fileName = "entry_", menuName = "Dialogue/Entry" )]
public sealed class DialogueEntryData : ScriptableObject
{
    // -- FIELDS

    [SerializeField, Multiline] private string _text = null;
    [Tooltip( "Number of characters per second" )]
    [SerializeField] private float _speed = 20f;
    [SerializeField] private EDialoguePosition _position = EDialoguePosition.BottomLeft;

    // -- PROPERTIES

    public string Text => _text;
    public float Speed => _speed;
    public EDialoguePosition Position => _position;
}
