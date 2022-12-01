using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    private Vector3 _startingPosition = Vector3.zero;

    // -- PROPERTIES

    public Transform Transform { get; private set; }

    public EnemyLife Life { get; private set; }
    public EnemyController Controller { get; private set; }

    public bool Enabled
    {
        set => gameObject.SetActive( value );
    }

    // -- METHODS

    private void EnemyLife_OnDied( EnemyLife entity_life )
    {
        Destroy( gameObject );
    }

    public void ResetEnemy()
    {
        Transform.localPosition = _startingPosition;
        Life.ChangeHealth( float.MaxValue );
    }

    // -- UNITY

    private void Awake()
    {
        Transform = transform;
        _startingPosition = Transform.localPosition;
        Life = GetComponent<EnemyLife>();
        Controller = GetComponent<EnemyController>();

        Enabled = false;
    }

    private void Start()
    {
        Life.OnDied += EnemyLife_OnDied;
    }

    private void OnDestroy()
    {
        Life.OnDied -= EnemyLife_OnDied;
    }
}
