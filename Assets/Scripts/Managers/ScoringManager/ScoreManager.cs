using UnityEngine;

public class ScoreManager{
    private int baseTargetScore = 200;
    private int _currentScore = 0;
    private float percentageCooled;
    private int scorePerBeer = 20;
    private int _scorePerWarmOne = 10;
    private int _currentLevel = 1;
    private float _fractionalMultiplier = 1.1f;
    

    public int ScorePerBeer { get => scorePerBeer; set => scorePerBeer = value; }
    public float PercentageCooled { get => percentageCooled; set => percentageCooled = value; }
    public int CurrentScore { get => _currentScore; set => _currentScore = value; }
    public int BaseTargetScore { get => baseTargetScore; set => baseTargetScore = value; }
    public int ScorePerWarmOne { get => _scorePerWarmOne; set => _scorePerWarmOne = value; }


    public int CurrentLevelTargetScore 
    {
        get => Mathf.RoundToInt(baseTargetScore * Mathf.Pow(_fractionalMultiplier, _currentLevel - 1));
    }
    public int CurrentLevel { get => _currentLevel; set => _currentLevel = value; }
    public float FractionalMultiplier { get => _fractionalMultiplier; set => _fractionalMultiplier = value; }

    public bool ScoreMet()
    {
        return _currentScore >= CurrentLevelTargetScore;
    }
}