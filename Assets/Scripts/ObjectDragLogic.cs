using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class ObjectDragLogic : MonoBehaviour
{
    [SerializeField] bool isDragging = false;
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
            transform.localScale -= new Vector3(0.3f, 0.3f, 0.3f);
            isDragging = false;
        }
        else
        {
            allChildren = GetComponentsInChildren<Transform>();
            transform.localScale += new Vector3(0.3f, 0.3f, 0.3f);
            isDragging = true;
        }
    }
}