using UnityEngine;

public sealed class SwordSlashEffector : AbilityEffector
{
    // -- TYPES

    [System.Serializable]
    private class DamageCircle
    {
        // -- FIELDS

        [SerializeField] private Transform _centerPoint = null;
        [SerializeField] private float _radius = 1f;

        public readonly Collider2D[] HitObjects = new Collider2D[ 5 ];

        // -- PROPERTIES

        public Transform CenterPoint => _centerPoint;
        public float Radius => _radius;

        // -- METHODS

        public int ActivateOverlap( LayerMask layer_mask )
        {
            return Physics2D.OverlapCircleNonAlloc( _centerPoint.position, _radius, HitObjects, layer_mask );
        }
    }

    // -- FIELDS

    [SerializeField] private DamageCircle[] _damageAreas = null;

#if UNITY_EDITOR
    [SerializeField] private bool _showGizmos = false;
#endif

    // -- METHODS

    public override void ActivateEffect( AbilityData ability_data )
    {
        SwordAttackData sword_attack_data = (SwordAttackData)ability_data;
        Vector3 player_position = Player.Instance.Transform.position;

        foreach( var damage_area in _damageAreas )
        {
            int enemy_number = damage_area.ActivateOverlap( LayerConstants.EnemiesMask );

            for( int enemy_index = 0; enemy_index < enemy_number; enemy_index++ )
            {
                Enemy enemy = damage_area.HitObjects[ enemy_index ].GetComponent<Enemy>();
                Vector2 knockback_direction = (enemy.Transform.position.ToVector2() - player_position.ToVector2()).normalized;

                enemy.Life.ChangeHealth( -sword_attack_data.Damage );
                enemy.Controller.AddForce( sword_attack_data.Knockback * knockback_direction, ForceMode2D.Impulse );
            }
        }
    }

    // -- UNITY

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if( !_showGizmos || _damageAreas == null || _damageAreas.Length == 0 )
        {
            return;
        }

        foreach( var damage_area in _damageAreas )
        {
            if( damage_area.CenterPoint == null )
            {
                continue;
            }

            Gizmos.DrawWireSphere( damage_area.CenterPoint.position, damage_area.Radius );
        }
    }
#endif
}
