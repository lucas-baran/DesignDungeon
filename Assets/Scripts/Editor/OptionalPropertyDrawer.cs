using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer( typeof( Optional<> ) )]
public sealed class OptionalPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(
        Rect position,
        SerializedProperty property,
        GUIContent label
    )
    {
        EditorGUI.BeginProperty( position, label, property );

        var enabled_property = property.FindPropertyRelative( "_Enabled" );
        var value_property = property.FindPropertyRelative( "_Value" );

        position.width -= 24;

        EditorGUI.BeginDisabledGroup( !enabled_property.boolValue );
        EditorGUI.PropertyField( position, value_property, label, true );
        EditorGUI.EndDisabledGroup();

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        position.x += position.width + 24;
        position.width = position.height = EditorGUI.GetPropertyHeight( enabled_property );
        position.x -= position.width;

        EditorGUI.PropertyField( position, enabled_property, GUIContent.none );

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(
        SerializedProperty property,
        GUIContent label
        )
    {
        var value_property = property.FindPropertyRelative( "_Value" );
        return EditorGUI.GetPropertyHeight( value_property );
    }
}
