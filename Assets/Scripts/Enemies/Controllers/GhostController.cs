using UnityEngine;

public sealed class GhostController : EnemyController
{
    // -- FIELDS

    [SerializeField, Min( 1f )] private float _teleportDistance = 10f;
    [SerializeField, Min( 0f )] private float _teleportDistanceVariance = 1f;

    private bool _activateAttack = false;

    // -- METHODS

    protected override void Attack()
    {
        if( !_activateAttack )
        {
            return;
        }

        _activateAttack = false;
        Player.Instance.Life.ChangeHealth( -1 );
        TeleportAfterAttack();
    }

    protected override void UpdateMovementStimulus()
    {
        Vector3 direction_to_player = (Player.Instance.Transform.position - transform.position).normalized;

        for( int stimulus_index = 0; stimulus_index < StimulusCount; stimulus_index++ )
        {
            var direction_stimulus = Vector3.Dot( direction_to_player, _movementDirections[ stimulus_index ] );
            _movementStimulus[ stimulus_index ] = direction_stimulus;
        }
    }

    private void TeleportAfterAttack()
    {
        Vector3 teleport_direction = (Player.Instance.CurrentRoom.RoomCenter - Player.Instance.Transform.position).normalized;
        Vector3 position_from_player = Random.Range( _teleportDistance - _teleportDistanceVariance, _teleportDistance + _teleportDistanceVariance ) * teleport_direction;

        transform.position = Player.Instance.Transform.position + position_from_player;
    }

    // -- UNITY

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.CompareTag( TagConstants.PlayerTag ) )
        {
            _activateAttack = true;
        }
    }
}
