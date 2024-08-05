using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{   
    [SerializeField] private Chunk _chunkTile;
    [SerializeField] private Chunk _endChunk;
    [SerializeField] private int _numberOfInitialChunks;
    [SerializeField] private float _initialChunkPositionAdjust = 0f;
    Transform nextChunkSpawn;
    private int _levelTotalChunksToSpawn;
    private int _chunksSpawned;
    private LinkedList<Chunk> _activeChunks = new LinkedList<Chunk>();
    private LinkedList<Chunk> _inactiveChunks = new LinkedList<Chunk>();

    void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitializeFirstChunks()
    {   
        Chunk initialChunk = Instantiate(_chunkTile);
        initialChunk.transform.position = gameObject.transform.position;
        _chunksSpawned++;
        initialChunk.transform.position += new Vector3(0,0,_initialChunkPositionAdjust);
        _SetNextChunkSpawn(initialChunk);
        ActiveChunksSetLast(initialChunk);

        for(int i = 1; _chunksSpawned < _numberOfInitialChunks; i++){
            Chunk newChunk = SpawnChunk();
            _chunksSpawned++;
            ActiveChunksSetLast(newChunk);
        }
    }
    public Chunk SpawnChunk()
    {   
        Chunk spawnedChunk = Instantiate(_chunkTile);
        spawnedChunk.transform.position = nextChunkSpawn.position;
        _SetNextChunkSpawn(spawnedChunk);
        return spawnedChunk;
    }
    public Chunk SpawnChunk(Chunk specialChunk)
    {
        Chunk spawnedChunk = Instantiate(specialChunk);
        spawnedChunk.transform.position = nextChunkSpawn.position;
        _SetNextChunkSpawn(spawnedChunk);
        return spawnedChunk;
    }
    private void _SetNextChunkSpawn(Chunk chunk)
    {
        nextChunkSpawn = chunk.GetNextChunkSpawn();
    }
    public void OnTriggerNextChunkOperations()
    {   Chunk nextChunk;
        if(_chunksSpawned + 1 == _levelTotalChunksToSpawn)
        {
            nextChunk = SpawnChunk(_endChunk);
            _chunksSpawned++;
        }
        else if(_chunksSpawned == _levelTotalChunksToSpawn)
        {
            return;
        }
        else{
            nextChunk = SpawnChunk();
            _chunksSpawned++;
        }
        ActiveChunksSetLast(nextChunk);

        if(_activeChunks.Count > 4){
            Chunk deactivatingChunk = ActiveChunksGetFirst();
            _ActiveChunksRemoveFirst();
            _InactiveChunksSetLast(deactivatingChunk);
        }
        
    }

    //add a spawned chunk to the active list
    public void ActiveChunksSetLast(Chunk spawnedChunk)
    {
        _activeChunks.AddLast(spawnedChunk);
    }
    //Add a despawend chunk to the inactive list's end
    private void _InactiveChunksSetLast(Chunk deactivatedChunk)
    {   deactivatedChunk.enabled = false;
        _inactiveChunks.AddLast(deactivatedChunk);
    }
    //get the oldest Active chunk (behind the player)
    public Chunk ActiveChunksGetFirst()
    {   return _activeChunks.First.Value;
    }

    public Chunk ActiveChunksGetLast()
    {
        return _activeChunks.Last.Value;
    }
    //remove the oldest active chunk
    private void _ActiveChunksRemoveFirst()
    {
        _activeChunks.RemoveFirst();
    }

    public void SetLevelTotalChunksToSpawn(int amount)
    {
        _levelTotalChunksToSpawn = amount;
    }
}
