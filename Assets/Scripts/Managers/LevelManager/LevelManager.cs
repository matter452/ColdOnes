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
    

    void Start()
    {   InitLevelConfig();
        _chunkSpawner = GetComponentInChildren<ChunkSpawner>();
        _scoringManager = GameManager.GetScoreManager();
    }

    public void InitLevelConfig()
    {
        CurrentLevel = 0;
        levelPassed = false;
    }
    public void StartLevel()
    {   CurrentLevel++;
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
            IncreaseLevelLength(1);
            _scoringManager.CurrentLevel++;
            _chunkSpawner.ResetChunkCount();
            _chunkSpawner.SetLevelTotalChunksToSpawn(levelLength);
            StartLevel();
            
        }
        else{
            CurrentLevel = 0;
            UIManager.Instance.DisplayUI(UIManager.e_UiDocuments.LevelStartUI);
        }
    }

}