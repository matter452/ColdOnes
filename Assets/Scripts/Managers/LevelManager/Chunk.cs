using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{ 
     [SerializeField] private Transform _nextChunkSpawn;
    [SerializeField] private GameObject[] _smallObjects;
    [SerializeField] private GameObject[] _mediumObjects;
    [SerializeField] private GameObject[] _largeObjects;
    [SerializeField] private List<GameObject> _xLObjects;
  /*   private BoxCollider _endOfChunkCollider;
    private ChunkGridManager _gridManager; */
    
    public Transform buildingLane1;
    public Transform buildingLane2;
    private Transform lane1;
    private Transform lane2;
    
    // Start is called before the first frame update
    void Start()
    {
       /*  _endOfChunkCollider = GetComponent<BoxCollider>(); */
        /* _gridManager = FindObjectOfType<ChunkGridManager>(); */
        /* lane1 = buildingLane1;
        lane2 = buildingLane2; */
        PlaceObjects(_xLObjects);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* private void PlaceObjects(GameObject[] objects, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = _gridManager.GetRandomPositionInChunk(transform);
            GameObject obj = Instantiate(objects[Random.Range(0, objects.Length)], buildingLane1, Quaternion.identity);
            obj.transform.parent = transform; 
        }
    } */

    private void PlaceObjects(List<GameObject> gameObjects)
    {
        Vector3 position1;
        Vector3 position2;
        for (int i = 0; i < 8; i++)
        {   
            position1 = new Vector3(0,0,(i+2)*20);
            position2 = new Vector3(0,0,(i+2)*20);
            if(i % 2 == 0)
            {
                continue;
            }
            Instantiate(gameObjects[Random.Range(0, gameObjects.Count)], buildingLane1.position+position1, Quaternion.Euler(0,90,0), buildingLane1);
            Instantiate(gameObjects[Random.Range(0, gameObjects.Count)], buildingLane2.position+position2, Quaternion.Euler(0,-90,0), buildingLane2);
            /* Instantiate(_xLargeObj[Random.Range(0, _xLargeObj.Length)], buildingLane2.position, Quaternion.Euler(0,-90,0), buildingLane2); */
            
        }
    }
/*     private void PlaceObjectsInChunk()
    {
        PlaceObjects(_xLargeObjects); 
    }
 */
    public Transform GetNextChunkSpawn()
    {
        return _nextChunkSpawn;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.GetLevelManager().SpawnNextChunks();
        }
    }

}