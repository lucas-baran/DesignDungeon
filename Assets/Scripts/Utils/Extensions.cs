using UnityEngine;

public static class Extensions
{
    public static Vector2 ToVector2( this Vector3 vector3 ) => new Vector2( vector3.x, vector3.y );
}
