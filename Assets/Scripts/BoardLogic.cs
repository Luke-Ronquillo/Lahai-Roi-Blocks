using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.Android;
using UnityEngine.Tilemaps;

public class BoardLogic : MonoBehaviour
{
    [SerializeField] RectInt bounds;
    [SerializeField] UnityEvent placedBlock;
    Tilemap tilemap;
    GameObject blockInside;
    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        blockInside = collision.gameObject;
    }
    public void IsValidPosition()
    {
        if (blockInside == null)
            return;
        Transform[] allPositions = blockInside.GetComponent<ObjectDragLogic>().allChildren;
        for (int i = 0; i < allPositions.Length; i++)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(allPositions[i].position);
            if (!bounds.Contains((Vector2Int)cellPosition))
            {
                return;
            }
            if (tilemap.HasTile(cellPosition))
            {
                return;
            }
        }
        SetTile();
    }
    public void SetTile()
    {
        Transform[] allPositions = blockInside.GetComponent<ObjectDragLogic>().allChildren;
        for (int i = 1; i < allPositions.Length; i++)
        {
            Vector3Int cellPosition = tilemap.WorldToCell(allPositions[i].position);
            Tile newTile = ScriptableObject.CreateInstance<Tile>();
            newTile.sprite = blockInside.GetComponentInChildren<SpriteRenderer>().sprite;
            tilemap.SetTile(cellPosition, newTile);
        }
        placedBlock.Invoke();
        CheckFullLines();
        Destroy(blockInside);
    }
    private void CheckFullLines()
    {
        int counter = 0;
        bool isVertical = true;
        // Vertical Checking
        for (int x = 0; x < bounds.height; x++)
        {
            for (int y = 0; y < bounds.width; y++)
            {
                if (!tilemap.HasTile((Vector3Int)bounds.position + new Vector3Int(x, y, 0)))
                {
                    break;
                }
                counter++;
            }
            if (counter == 10)
            {
                ClearLine(x, isVertical);
            }
            counter = 0;
        }
        // Horizontal Checking
        isVertical = false;
        for (int y = 0; y < bounds.height; y++)
        {
            for (int x = 0; x < bounds.width; x++)
            {
                if (!tilemap.HasTile((Vector3Int)bounds.position + new Vector3Int(x, y, 0)))
                {
                    break;
                }
                counter++;
            }
            if (counter == 10)
            {
                ClearLine(y, isVertical);
            }
            counter = 0;
        }
    }
    private void ClearLine(int i, bool isVertical)
    {
        if (isVertical)
        {
            for (int y = 0; y < bounds.height; y++)
            {
                tilemap.SetTile((Vector3Int)bounds.position + new Vector3Int(i, y, 0), null);
            }
        }
        else
        {
            for (int x = 0; x < bounds.height; x++)
            {
                tilemap.SetTile((Vector3Int)bounds.position + new Vector3Int(x, i, 0), null);
            }
        }
    }
}