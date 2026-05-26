using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectDragLogic : MonoBehaviour
{
    [SerializeField] bool isDragging = false;

    Vector3 originalPos;
    private void Awake()
    {
        originalPos = transform.position;
    }
    private void Update()
    {
        if (isDragging)
        {
            transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        }
    }
    public void OnClick()
    {
        if (isDragging)
        {
            transform.position = originalPos;
            transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
            isDragging = false;
        }
        else
        {
            transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
            isDragging = true;
        }
    }
}
