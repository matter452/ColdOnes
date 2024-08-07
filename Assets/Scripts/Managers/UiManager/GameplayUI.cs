using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameplayUI : MonoBehaviour
{
    private UIManager _uiManager;
    private VisualElement _root;
    private Label _coldOnes;
    private Label _warmOnes;
    private Label _targetScore;
    private Label _currentScore;
    private LevelManager _levelManager;


    void Start()
    {
        InitElements();
        _levelManager = GameManager.GetLevelManager();
        SubscribeToLevelManagerEvents();
        
        if (_coldOnes == null || _warmOnes == null || _targetScore == null || _currentScore == null)
        {
            Debug.LogError("UI elements not found");
            return;
        }

        /* UpdateColdOnes(_levelManager.ColdOnes);
        UpdateWarmOnes(_levelManager.WarmOnes); */
        //UpdateScore(_levelManager.CurrentScore);
/*         UpdateTargetScore(_levelManager.CurrentScore, _levelManager.CurrentLevelTargetScore); */
    }

    void Update()
    {
        /* UpdateProgressBar(); */
    }

    public void SetRootElement(UIManager uIManager, VisualElement activeUIRoot)
    {
        _uiManager = uIManager;
        _root = activeUIRoot;
    }

    private void UpdateColdOnes(int newCountColds)
    {
        _coldOnes.text = "Cold Ones: " + newCountColds.ToString();
    }

    private void UpdateWarmOnes(int newCountWarms)
    {
        _warmOnes.text = "Warm Ones: " + newCountWarms.ToString();
    }

    private void UpdateScore(int newScore)
    {
        _currentScore.text = "Score: " + newScore.ToString();
    }

    private void UpdateTargetScore(int currentScore, int targetScore)
    {
        _targetScore.text = currentScore.ToString() + " / " + targetScore.ToString();
    }

    private void SubscribeToLevelManagerEvents()
    {
        _levelManager.onColdsChanged += UpdateColdOnes;
        _levelManager.onWarmsChanged += UpdateWarmOnes;
        _levelManager.onScoreChanged += UpdateScore;
        _levelManager.onScoreChanged += (newScore) => UpdateTargetScore(newScore, _levelManager.CurrentLevelTargetScore);
    }

    private void UnsubscribeFromLevelManagerEvents()
    {
        _levelManager.onColdsChanged -= UpdateColdOnes;
        _levelManager.onWarmsChanged -= UpdateWarmOnes;
        _levelManager.onScoreChanged -= UpdateScore;
        _levelManager.onScoreChanged -= (newScore) => UpdateTargetScore(newScore, _levelManager.CurrentLevelTargetScore);
    }

    private void OnDestroy()
    {
        UnsubscribeFromLevelManagerEvents();
    }

    private void InitElements()
    {
        if (_root == null)
        {
            Debug.LogError("_root is null");
            return;
        }

        _coldOnes = _root.Q<Label>("coldOnes");
        _warmOnes = _root.Q<Label>("warmOnes");
        _targetScore = _root.Q<Label>("targetScore");
        _currentScore = _root.Q<Label>("currentScore");
    }
}