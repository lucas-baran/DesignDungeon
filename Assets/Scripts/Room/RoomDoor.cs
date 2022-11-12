using UnityEngine;
using UnityEngine.Assertions;

public sealed class RoomDoor : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private Transform _entrancePoint = null;
    [SerializeField] private DoorData _doorData = null;
    [SerializeField] private DoorData _linkedDoorData = null;

    // -- PROPERTIES

    public Room Room { get; set; }
    public DoorData LinkedDoorData => _linkedDoorData;
    public Vector3 EntrancePosition => _entrancePoint.position;

    // -- UNITY

    private void Start()
    {
        Assert.IsFalse( _doorData == null, $"The door {name} in scene {gameObject.scene} doesn't have DoorData!" );
        Assert.IsFalse( _linkedDoorData == null, $"The door {name} in scene {gameObject.scene} isn't linked to any door!" );

        _doorData.Door = this;
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.CompareTag( TagConstants.PlayerTag ) )
        {
            Player.Instance.PlayerInput.Lock();
            Player.Instance.PlayerController.ResetVelocity();

            _doorData.Enter();
        }
    }
}
