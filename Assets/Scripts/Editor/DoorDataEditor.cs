using UnityEditor;

[CustomEditor( typeof( DoorData ) )]
public sealed class DoorDataEditor : Editor
{
    // -- FIELDS

    private SerializedProperty _linkedRoomDataProperty = null;
    private SerializedProperty _selectedLinkedDoorDataIndexProperty = null;

    private RoomData[] _roomDataInstances = null;

    // -- UNITY

    private void OnEnable()
    {
        _linkedRoomDataProperty = serializedObject.FindProperty( "_linkedRoomData" );
        _selectedLinkedDoorDataIndexProperty = serializedObject.FindProperty( "_selectedLinkedDoorDataIndex" );

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
        int seleted_option_index = 0;

        for( int room_options_index = 0; room_options_index < room_options.Length; room_options_index++ )
        {
            RoomData room_data = _roomDataInstances[ room_options_index ];

            room_options[ room_options_index ] = room_data.name;

            if( room_data == (RoomData)_linkedRoomDataProperty.objectReferenceValue )
            {
                seleted_option_index = room_options_index;
            }
        }

        seleted_option_index = EditorGUILayout.Popup( "Linked Door Data", seleted_option_index, room_options );

        RoomData selected_room_data = _roomDataInstances[ seleted_option_index ];
        _linkedRoomDataProperty.objectReferenceValue = selected_room_data;

        if( selected_room_data != null
            && selected_room_data.Doors != null
            && selected_room_data.Doors.Length > 0
            )
        {
            string[] door_options = new string[ selected_room_data.Doors.Length ];

            for( int door_data_index = 0; door_data_index < door_options.Length; door_data_index++ )
            {
                DoorData door_data = selected_room_data.Doors[ door_data_index ];
                door_options[ door_data_index ] = door_data.name;
            }

            _selectedLinkedDoorDataIndexProperty.intValue = EditorGUILayout.Popup( "Linked Door Data", _selectedLinkedDoorDataIndexProperty.intValue, door_options );
        }

        serializedObject.ApplyModifiedProperties();
    }
}
