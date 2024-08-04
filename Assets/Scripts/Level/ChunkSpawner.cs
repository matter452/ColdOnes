using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{   
    [SerializeField] private Chunk _chunkTile;
    [SerializeField] private int _numberOfInitialChunks;
    Transform nextChunkSpawn;
    private LinkedList<Chunk> _activeChunks = new LinkedList<Chunk>();
    private LinkedList<Chunk> _inactiveChunks = new LinkedList<Chunk>();

    void Awake()
    {
        InitializeFirstChunks();
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnChunk();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitializeFirstChunks()
    {   
        Chunk initialChunk = Instantiate(_chunkTile, transform);
        _SetNextChunkSpawn(initialChunk);
        ActiveChunksSetLast(initialChunk);

        for(int i = 0; i < _numberOfInitialChunks; i++){
            Chunk newChunk = SpawnChunk();
            ActiveChunksSetLast(newChunk);
        }
    }
    public Chunk SpawnChunk()
    {   
        Chunk spawnedChunk = Instantiate(_chunkTile, nextChunkSpawn);
        _SetNextChunkSpawn(spawnedChunk);
        return spawnedChunk;
    }
    private void _SetNextChunkSpawn(Chunk chunk)
    {
        nextChunkSpawn = chunk.GetNextChunkSpawn();
    }
    public void TriggerNextChunkOperations()
    {   Chunk nextChunk = SpawnChunk();
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
    {
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
}
