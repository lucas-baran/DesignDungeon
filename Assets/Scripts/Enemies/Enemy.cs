using UnityEngine;

public sealed class Enemy : MonoBehaviour
{
    // -- PROPERTIES

    public EnemyLife Life { get; private set; }

    // -- METHODS

    // -- UNITY

    private void Awake()
    {
        Life = GetComponent<EnemyLife>();
    }
}
