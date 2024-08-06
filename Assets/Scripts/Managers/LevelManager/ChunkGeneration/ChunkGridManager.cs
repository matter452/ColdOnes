using UnityEngine;
public class ChunkGridManager : MonoBehaviour
{
    public int ChunkWidth = 13;
    public int ChunkHeight = 20;

    private int[,] grid;

    void Start()
    {
        InitializeGrid();
    }

    public void InitializeGrid()
    {
        grid = new int[ChunkWidth, ChunkHeight];
        for (int x = 0; x < ChunkWidth; x++)
        {
            for (int z = 0; z < ChunkHeight; z++)
            {
                grid[x, z] = 0; //cell is empty
            }
        }
    }

    public bool CanPlaceObject(Vector3 position, ChunkUnitData sizeInfo)
    {
        int startX = (int)position.x;
        int startY = (int)position.z;

        if (startX + sizeInfo.WidthInCells > ChunkWidth || startY + sizeInfo.HeightInCells > ChunkHeight)
        {
            return false;
        }

        for (int x = startX; x < startX + sizeInfo.WidthInCells; x++)
        {
            for (int y = startY; y < startY + sizeInfo.HeightInCells; y++)
            {
                if (grid[x, y] != 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public Vector3 GetRandomPositionInChunk(Transform chunkTransform)
    {
        float chunkWidth = ChunkWidth;
        float chunkHeight = ChunkHeight;

        float x = Random.Range(chunkTransform.position.x - chunkWidth / 2, chunkTransform.position.x + chunkWidth / 2);
        float y = 0;
        float z = Random.Range(chunkTransform.position.z - chunkHeight / 2, chunkTransform.position.z + chunkHeight / 2);

        return new Vector3(x, y, z);
    }

    public void PlaceObject(Vector3 position, ChunkUnitData sizeInfo)
    {
        int startX = (int)position.x;
        int startY = (int)position.z;

        for (int x = startX; x < startX + sizeInfo.WidthInCells; x++)
        {
            for (int y = startY; y < startY + sizeInfo.HeightInCells; y++)
            {
                grid[x, y] = 1; // Mark cell as filled
            }
        }
    }
}
