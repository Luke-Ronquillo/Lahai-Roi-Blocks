using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    IA_Player playerActions;
    Camera mainCamera;
    private void Awake()
    {
        playerActions = new IA_Player();
        mainCamera = Camera.main;
    }
    private void OnEnable()
    {
        playerActions.Enable();
        playerActions.User.OnLeftClick.performed += OnClick;
    }
    private void OnDisable()
    {
        playerActions.User.OnLeftClick.performed -= OnClick;
        playerActions.Disable();
    }
    public void OnClick(InputAction.CallbackContext ctx)
    {
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit != null)
        {
            Debug.Log("Clicked On: " + hit.name);
            if (hit.GetComponent<ObjectDragLogic>() != null)
            {
                hit.GetComponent<ObjectDragLogic>().OnClick();
            }
        }
        else
        {
            Debug.Log("Clicked on Empty Space");
        }
    }
}
