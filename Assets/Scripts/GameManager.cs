using UnityEngine;

public sealed class GameManager : MonoBehaviour
{
    // -- PROPERTIES

    public static GameManager Instance { get; private set; }

    public Player Player { get; private set; }

    // -- UNITY

    private void Awake()
    {
        if( Instance == null )
        {
            Instance = this;

            Player = FindObjectOfType<Player>();
        }
        else
        {
            Destroy( this );
        }
    }
}
