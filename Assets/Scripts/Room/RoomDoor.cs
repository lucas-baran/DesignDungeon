using UnityEngine;
using UnityEngine.Assertions;

public sealed class RoomDoor : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private Transform _entrancePoint = null;
    [SerializeField] private DoorData _doorData = null;

    // -- PROPERTIES

    public DoorData DoorData => _doorData;
    public Vector3 EntrancePosition => _entrancePoint.position;

    // -- EVENTS

    public delegate void DoorEnteredHandler( RoomDoor door );
    public event DoorEnteredHandler OnDoorEnter = null;

    // -- UNITY

    private void Awake()
    {
        Assert.IsFalse( _doorData == null, $"The door {name} in scene {gameObject.scene} doesn't have DoorData!" );

        _doorData.Door = this;
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.CompareTag( TagConstants.PlayerTag ) )
        {
            Player.Instance.PlayerInput.Lock();
            Player.Instance.PlayerController.ResetVelocity();

            OnDoorEnter?.Invoke( this );
        }
    }
}
