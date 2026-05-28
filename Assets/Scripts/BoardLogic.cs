using UnityEngine;
using UnityEngine.Tilemaps;

public class BoardLogic : MonoBehaviour
{
    [SerializeField] RectInt bounds;
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
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    blockInside = null;
    //}
    public void IsValidPosition()
    {
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
        Destroy(blockInside);
    }
}