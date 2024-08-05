using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chunk : MonoBehaviour
{   [SerializeField] private Transform _nextChunkSpawn;
    private BoxCollider _endOfChunkCollider;
    
    
    // Start is called before the first frame update
    void Start()
    {
        _endOfChunkCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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