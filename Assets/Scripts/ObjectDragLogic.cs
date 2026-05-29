using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ObjectDragLogic : MonoBehaviour
{
    [SerializeField] bool isDragging = false;
    [SerializeField] float scaleFactor = 0.5f;
    UnityEvent OnPlace;
    BoardLogic boardLogic;

    public Transform[] allChildren { get; private set; }

    Vector3 originalPos;
    private void Awake()
    {
        originalPos = transform.position;
        allChildren = GetComponentsInChildren<Transform>();
        boardLogic = FindAnyObjectByType<BoardLogic>();

        OnPlace = new UnityEvent();
        OnPlace.AddListener(boardLogic.IsValidPosition);
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
            OnPlace.Invoke();
            transform.position = originalPos;
            transform.localScale -= new Vector3(scaleFactor, scaleFactor, scaleFactor);
            isDragging = false;
        }
        else
        {
            allChildren = GetComponentsInChildren<Transform>();
            transform.localScale += new Vector3(scaleFactor, scaleFactor, scaleFactor);
            isDragging = true;
        }
    }
    public void OnRotate()
    {
        if (isDragging)
        {
            transform.Rotate(new Vector3(0f, 0f, 90f));
        }
    }
}