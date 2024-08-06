using UnityEngine;

public class ScoreManager
{
    private int baseTargetScore = 200;
    private int _currentScore = 0;
    private float _percentageCooled;
    private int _scorePerColdOne = 20;
    private int _scorePerWarmOne = 10;
    private int _currentLevel = 1;
    private float _fractionalMultiplier = .1f;
    private int _currentLevelTargetScore;

    public int ScorePerColdOne { get => _scorePerColdOne; set => _scorePerColdOne = value; }
    public float PercentageCooled { get => _percentageCooled; set => _percentageCooled = value; }
    public int CurrentScore { get => _currentScore; set => _currentScore = value; }
    public int BaseTargetScore { get => baseTargetScore; set => baseTargetScore = value; }
    public int ScorePerWarmOne { get => _scorePerWarmOne; set => _scorePerWarmOne = value; }

    public int CurrentLevelTargetScore
    {
        get => CalculateCurrentLevelTargetScore();
        private set => _currentLevelTargetScore = value;
    }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public float FractionalMultiplier { get => _fractionalMultiplier; set => _fractionalMultiplier = value; }

    private int CalculateCurrentLevelTargetScore()
    {
        return Mathf.RoundToInt(_currentScore + 200 + (BaseTargetScore * _fractionalMultiplier * CurrentLevel));
    }

    public bool ScoreMet()
    {
        return _currentScore >= CurrentLevelTargetScore;
    }

    public void IncrementTargetScore()
    {
        CurrentLevelTargetScore = CalculateCurrentLevelTargetScore();
    }
}
