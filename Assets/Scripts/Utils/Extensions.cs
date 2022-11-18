using UnityEditor;
using UnityEngine;

public static class Extensions
{
    public static Vector2 ToVector2( this Vector3 vector3 ) => new Vector2( vector3.x, vector3.y );

#if UNITY_EDITOR
    public static T[] GetAllInstances<T>() where T : ScriptableObject
    {
        string[] guids = AssetDatabase.FindAssets( "t:" + typeof( T ).Name );
        T[] instances = new T[ guids.Length ];

        for( int i = 0; i < guids.Length; i++ )
        {
            string path = AssetDatabase.GUIDToAssetPath( guids[ i ] );
            instances[ i ] = AssetDatabase.LoadAssetAtPath<T>( path );
        }

        return instances;
    }
#endif
}
