using UnityEngine;

public class HideOnPlay : MonoBehaviour
{
    // -- TYPES

    public enum EHideOn
    {
        DontHide = 0,
        Awake = 1,
        Start = 2,
    }

    // -- FIELDS

    [SerializeField] private EHideOn _hideOn = EHideOn.Start;

    // -- UNITY

    protected virtual void Awake()
    {
        if( _hideOn == EHideOn.Awake )
        {
            gameObject.SetActive( false );
        }
    }

    protected virtual void Start()
    {
        if( _hideOn == EHideOn.Start )
        {
            gameObject.SetActive( false );
        }
    }
}
