using UnityEngine;

public sealed class DamageObject : MonoBehaviour
{
    // -- FIELDS

    [SerializeField, Range( 1, 5 )] private int _damage = 1;

    // -- UNITY

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if( collision.CompareTag( TagConstants.PlayerTag ) )
        {
            Player.Instance.Life.ChangeHealth( -_damage );
        }
    }
}
