using UnityEditor;

[CustomEditor( typeof( DoorData ) )]
public sealed class DoorDataEditor : Editor
{
    // -- FIELDS

    private SerializedProperty _linkedRoomDataProperty = null;
    private SerializedProperty _linkedDoorDataProperty = null;

    private RoomData[] _roomDataInstances = null;

    // -- UNITY

    private void OnEnable()
    {
        _linkedRoomDataProperty = serializedObject.FindProperty( "_linkedRoomData" );
        _linkedDoorDataProperty = serializedObject.FindProperty( "_linkedDoorData" );

        _roomDataInstances = Extensions.GetAllInstances<RoomData>();
    }

    public override void OnInspectorGUI()
    {
        if( _roomDataInstances.Length == 0 )
        {
            return;
        }

        serializedObject.Update();

        string[] room_options = new string[ _roomDataInstances.Length ];
        int seleted_room_option_index = 0;
        RoomData selected_room_data = (RoomData)_linkedRoomDataProperty.objectReferenceValue;

        for( int room_option_index = 0; room_option_index < room_options.Length; room_option_index++ )
        {
            RoomData room_data = _roomDataInstances[ room_option_index ];
            room_options[ room_option_index ] = room_data.name;

            if( room_data == selected_room_data )
            {
                seleted_room_option_index = room_option_index;
            }
        }

        seleted_room_option_index = EditorGUILayout.Popup( "Linked Door", seleted_room_option_index, room_options );

        selected_room_data = _roomDataInstances[ seleted_room_option_index ];
        _linkedRoomDataProperty.objectReferenceValue = selected_room_data;

        if( selected_room_data != null
            && selected_room_data.Doors != null
            && selected_room_data.Doors.Length > 0
            )
        {
            string[] door_options = new string[ selected_room_data.Doors.Length ];
            int seleted_door_option_index = 0;
            DoorData selected_door_data = (DoorData)_linkedDoorDataProperty.objectReferenceValue;

            for( int door_option_index = 0; door_option_index < door_options.Length; door_option_index++ )
            {
                DoorData door_data = selected_room_data.Doors[ door_option_index ];
                door_options[ door_option_index ] = door_data.name;

                if( door_data == selected_door_data )
                {
                    seleted_door_option_index = door_option_index;
                }
            }

            seleted_door_option_index = EditorGUILayout.Popup( "Linked Door", seleted_door_option_index, door_options );

            selected_door_data = selected_room_data.Doors[ seleted_door_option_index ];
            _linkedDoorDataProperty.objectReferenceValue = selected_door_data;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
