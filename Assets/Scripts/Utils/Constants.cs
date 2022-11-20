using UnityEngine;

public static class TagConstants
{
    public const string PlayerTag = "Player";
    public const string InteractableObjectTag = "InteractableObject";
}

public static class LayerConstants
{
    private const string EnemiesLayerName = "Enemy";

    public static readonly LayerMask EnemiesMask = LayerMask.GetMask( EnemiesLayerName );
}
