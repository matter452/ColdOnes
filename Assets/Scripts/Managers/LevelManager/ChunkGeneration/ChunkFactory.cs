using UnityEngine;
public class ChunkGenerator : MonoBehaviour
{
    public ChunkGridManager GridManager;
    public GameObject[] SmallObjects;
    public GameObject[] MediumObjects;
    public GameObject[] LargeObjects;
    public GameObject[] XLargeObjects;

    void Start()
    {
        GenerateChunk();
    }

    void GenerateChunk()
    {
        GridManager.InitializeGrid();
    ChunkUnitData[] allSizes = new ChunkUnitData[]
    {
        ChunkUnits.Small,
        ChunkUnits.Medium,
        ChunkUnits.Large,
        ChunkUnits.XLarge
    };

        for (int x = 0; x < GridManager.ChunkWidth; x++)
        {
            for (int y = 0; y < GridManager.ChunkHeight; y++)
            {
                Vector3 gridPosition = new Vector3(x*13, 0, y*20);

                ChunkUnitData sizeInfo = allSizes[Random.Range(0, allSizes.Length)];

                if (GridManager.CanPlaceObject(gridPosition, sizeInfo))
                {
                    PlaceObjectAt(gridPosition, sizeInfo);
                }
            }
        }
    }

    void GenerateEdge()
    {
        
    }

    void PlaceObjectAt(Vector3 position, ChunkUnitData sizeInfo)
    {
        GameObject obj = GetRandomObject(sizeInfo);
        Instantiate(obj, position, Quaternion.identity);
        GridManager.PlaceObject(position, sizeInfo);
    }

    void PlaceAlongEdge(Vector3 position)
    {
        GameObject obj = XLargeObjects[0];
        Instantiate(obj, position, Quaternion.identity);
    }
    GameObject GetRandomObject(ChunkUnitData sizeInfo)
    {
        GameObject[] objects = GetObjectArray(sizeInfo);
        return objects[Random.Range(0, objects.Length)];
    }

    GameObject[] GetObjectArray(ChunkUnitData sizeInfo)
    {
        switch (sizeInfo.Size)
        {
            case ChunkUnit.Small: return SmallObjects;
            case ChunkUnit.Medium: return MediumObjects;
            case ChunkUnit.Large: return LargeObjects;
            case ChunkUnit.XLarge: return XLargeObjects;
            default: return SmallObjects;
        }
    }
}
