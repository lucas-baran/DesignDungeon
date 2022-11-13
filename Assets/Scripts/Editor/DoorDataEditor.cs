using System.Collections.Generic;
using UnityEditor;

[CustomEditor( typeof( DoorData ) )]
public sealed class DoorDataEditor : Editor
{
    // -- FIELDS

    private SerializedProperty _linkedRoomDataProperty = null;
    private SerializedProperty _selectedLinkedDoorDataIndexProperty = null;

    // -- UNITY

    private void OnEnable()
    {
        _linkedRoomDataProperty = serializedObject.FindProperty( "_linkedRoomData" );
        _selectedLinkedDoorDataIndexProperty = serializedObject.FindProperty( "_selectedLinkedDoorDataIndex" );
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField( _linkedRoomDataProperty );

        RoomData linked_room_data = (RoomData)_linkedRoomDataProperty.objectReferenceValue;

        if( linked_room_data != null
            && linked_room_data.Doors != null
            && linked_room_data.Doors.Length > 0
            )
        {
            List<string> options = new List<string>( linked_room_data.Doors.Length );

            foreach( var door_data in linked_room_data.Doors )
            {
                options.Add( door_data.name );
            }

            _selectedLinkedDoorDataIndexProperty.intValue = EditorGUILayout.Popup( "Linked Door Data", _selectedLinkedDoorDataIndexProperty.intValue, options.ToArray() );
        }

        serializedObject.ApplyModifiedProperties();
    }
}
