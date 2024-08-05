using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{   
    public int CurrentLevel;
    public Time roundTime;
    private ScoreManager _scoringManager;
    public bool levelPassed;
    public int levelLength;
    private ChunkSpawner _chunkSpawner;
    

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _chunkSpawner = GetComponentInChildren<ChunkSpawner>();
        InitLevelConfig();
        _scoringManager = GameManager.ScoreManager;
    }

    public void InitLevelConfig()
    {
        CurrentLevel = 1;
        levelPassed = false;
    }
    public void StartLevel()
    {
        _chunkSpawner.SetLevelTotalChunksToSpawn(levelLength);
        _chunkSpawner.InitializeFirstChunks();
    }

    public void SpawnNextChunks()
    {
        _chunkSpawner.OnTriggerNextChunkOperations();
    }

    public void IncreaseLevelLength(int amount)
    {
        levelLength += amount;
    }

    public void ScoreButtonClicked()
    {
        if(_scoringManager.ScoreMet())
        {
           UIManager.Instance.DisplayUI(UIManager.e_UiDocuments.UpgradesUI); 
        }
        else{
            UIManager.Instance.DisplayUI(UIManager.e_UiDocuments.LevelStartUI);
        }
    }

}