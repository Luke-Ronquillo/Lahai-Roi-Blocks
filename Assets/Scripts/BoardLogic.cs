using UnityEngine;
using UnityEngine.Events;
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
        //placedBlock = new UnityEvent();
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
        Destroy(blockInside);
    }
}