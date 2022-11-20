using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    // -- PROPERTIES

    public Transform Transform { get; private set; }

    public EnemyLife Life { get; private set; }
    public EnemyController Controller { get; private set; }

    // -- METHODS

    private void EnemyLife_OnDied( EnemyLife entity_life )
    {
        Destroy( gameObject );
    }

    // -- UNITY

    private void Awake()
    {
        Transform = transform;
        Life = GetComponent<EnemyLife>();
        Controller = GetComponent<EnemyController>();
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
