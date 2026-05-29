using System;
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
        playerActions.User.OnLeftClick.performed += OnLeftClick;
        playerActions.User.OnRightClick.performed += OnRightClick;
    }
    private void OnDisable()
    {
        playerActions.User.OnRightClick.performed -= OnRightClick;
        playerActions.User.OnLeftClick.performed -= OnLeftClick;
        playerActions.Disable();
    }

    private void OnRightClick(InputAction.CallbackContext context)
    {
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit.GetComponent<ObjectDragLogic>() != null)
        {
            hit.GetComponent<ObjectDragLogic>().OnRotate();
        }
    }
    private void OnLeftClick(InputAction.CallbackContext ctx)
    {
        Vector2 worldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Collider2D hit = Physics2D.OverlapPoint(worldPosition);

        if (hit.GetComponent<ObjectDragLogic>() != null)
        {
            hit.GetComponent<ObjectDragLogic>().OnClick();
        }
    }
    
}