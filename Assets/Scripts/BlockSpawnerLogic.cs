using NUnit.Framework;
using UnityEngine;

public class BlockSpawnerLogic : MonoBehaviour
{
    [SerializeField]
    BlockTypeScriptableObject blockTypeScriptableObject;
    [SerializeField]
    Vector2[] blockSpawnPoints;

    int blockPlacedCount;
    GameObject[] blockTypes;
    private void Awake()
    {
        blockTypes = blockTypeScriptableObject.BlockTypes;
        blockPlacedCount = blockSpawnPoints.Length;
        PlacedBlock();
    }
    public void PlacedBlock()
    {
        blockPlacedCount++;
        if (blockPlacedCount >= blockSpawnPoints.Length)
        {
            int randomBlockIndex;
            for (int i = 0; i < blockSpawnPoints.Length; i++)
            {
                randomBlockIndex = Random.Range(0, blockTypes.Length);
                Instantiate(blockTypes[randomBlockIndex], blockSpawnPoints[i], Quaternion.identity);
            }
            blockPlacedCount = 0;
        }
    }
}
