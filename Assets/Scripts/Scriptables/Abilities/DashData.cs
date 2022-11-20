using UnityEngine;

[CreateAssetMenu( fileName = "Dash", menuName = "Skills/Dash" )]
public sealed class DashData : AbilityData
{
    // -- FIELDS

    [SerializeField] private float _dashVelocity = 50f;
    [SerializeField] private bool _invincibility = true;

    // -- PROPERTIES

    public float DashVelocity => _dashVelocity;
    public bool Invincibility => _invincibility;
}
