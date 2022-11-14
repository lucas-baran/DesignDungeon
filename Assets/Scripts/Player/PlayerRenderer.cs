using UnityEngine;

public sealed class PlayerRenderer : MonoBehaviour
{
    // -- FIELDS

    [SerializeField] private SpriteRenderer _canInteractRenderer = null;

    // -- METHODS

    private void PlayerController_OnCanInteractStateChanged( bool can_interact )
    {
        _canInteractRenderer.enabled = can_interact;
    }

    // -- UNITY

    private void Awake()
    {
        _canInteractRenderer.enabled = false;
    }

    private void Start()
    {
        Player.Instance.Controller.OnCanInteractStateChanged += PlayerController_OnCanInteractStateChanged;
    }

    private void OnDestroy()
    {
        Player.Instance.Controller.OnCanInteractStateChanged -= PlayerController_OnCanInteractStateChanged;
    }
}
