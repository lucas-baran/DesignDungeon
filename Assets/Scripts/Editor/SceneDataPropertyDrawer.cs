using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomPropertyDrawer( typeof( SceneData ) )]
public sealed class SceneDataPropertyDrawer : PropertyDrawer
{
    // -- UNITY

    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
    {
        EditorGUI.BeginProperty( position, label, property );

        if( SceneManager.sceneCountInBuildSettings > 0 )
        {
            SerializedProperty build_index_property = property.FindPropertyRelative( "_buildIndex" );

            string[] options = new string[ SceneManager.sceneCountInBuildSettings ];

            for( int scene_index = 0; scene_index < SceneManager.sceneCountInBuildSettings; scene_index++ )
            {
                var scene_path = SceneUtility.GetScenePathByBuildIndex( scene_index );
                var scene_name = Path.GetFileNameWithoutExtension( scene_path );
                options[ scene_index ] = scene_name;
            }

            Rect scene_label_rect = new Rect( position.x, position.y, 100, position.height );
            Rect build_index_rect = new Rect( position.x + 105, position.y, position.width - 105, position.height );

            EditorGUI.LabelField( scene_label_rect, new GUIContent( "Scene" ) );
            build_index_property.intValue = EditorGUI.Popup( build_index_rect, build_index_property.intValue, options );
        }

        EditorGUI.EndProperty();
    }
}
