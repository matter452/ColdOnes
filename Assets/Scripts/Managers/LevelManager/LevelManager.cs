using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int CurrentLevel;
    public float roundTime;
    private IceChest _iceChest;
    public bool levelPassed;
    public int levelLength;
    private ChunkSpawner _chunkSpawner;

    public int ColdOnes => _iceChest.TotalColdBeers;
    public int WarmOnes => _iceChest.TotalWarmBeers;
    public int CurrentScore => _iceChest.CurrentScore;
    public int CurrentLevelTargetScore => _iceChest.LevelTargetScore;

    public event Action<int> onColdsChanged;
    public event Action<int> onWarmsChanged;
    public event Action<int> onScoreChanged;

    void Start()
    {
        InitLevelConfig();
        _chunkSpawner = GetComponentInChildren<ChunkSpawner>();

    }

    private void OnDestroy()
    {
        if (_iceChest != null)
        {
            // Unsubscribe from IceChest events
            _iceChest.onColdsChanged -= HandleColdsChanged;
            _iceChest.onWarmsChanged -= HandleWarmsChanged;
            _iceChest.onScoreChanged -= HandleScoreChanged;
        }
    }

    public void InitLevelConfig()
    {
        CurrentLevel = 0;
        levelPassed = false;
        roundTime = 0;
        levelLength = 3;
    }

    public void InitiWorld()
    {   
        CurrentLevel++;
        _chunkSpawner.SetLevelTotalChunksToSpawn(levelLength);
        _chunkSpawner.InitializeFirstChunks();
    }

    public void ReInitWorld()
    {   _chunkSpawner.ResetChunkCount();
        _chunkSpawner.DeChunkLevel();
        InitiWorld();
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
        if (CurrentScore >= CurrentLevelTargetScore)
        {
            UIManager.Instance.DisplayUI(UIManager.e_UiDocuments.UpgradesUI);
            IncreaseLevelLength(1);
            
        }
        else
        {
            CurrentLevel = 0;
            UIManager.Instance.DisplayUI(UIManager.e_UiDocuments.LevelStartUI);
        }
    }

    private void HandleColdsChanged(int newCountColds)
    {
        onColdsChanged?.Invoke(newCountColds);
    }

    private void HandleWarmsChanged(int newCountWarms)
    {
        onWarmsChanged?.Invoke(newCountWarms);
    }

    private void HandleScoreChanged(int newScore)
    {
        onScoreChanged?.Invoke(newScore);
    }

    public void SetIceChest(PlayerManager player)
    {
        _iceChest = GameManager.Instance.GetPlayer().GetPlayerIceChest();
         if (_iceChest != null)
        {   
            // Subscribe to IceChest events 
            _iceChest.onColdsChanged += HandleColdsChanged;
            _iceChest.onWarmsChanged += HandleWarmsChanged;
            _iceChest.onScoreChanged += HandleScoreChanged;
        }
        else
        {
            Debug.Log("Ice chest tweaking");
        }
        
    }
}
