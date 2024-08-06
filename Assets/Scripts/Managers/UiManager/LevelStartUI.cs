using UnityEngine;
using UnityEngine.UIElements;

public class LevelStartUI : MonoBehaviour
{
private VisualElement _root;
private Label _levelNumber;
private Label _targetScore;
private Label _currentScore;
private Button _startButton;
private UIManager _uiManager;
private PlayerManager _playerManager;
private ScoreManager _scoreManager;
private LevelManager _levelManager;

    void Start()
    {
        InitElements();
        _levelManager = GameManager.GetLevelManager();
        _scoreManager = GameManager.GetScoreManager();
        _playerManager = GameManager.GetPlayerManager();
        _levelNumber.text = "Level " + _levelManager.CurrentLevel.ToString();
        _currentScore.text = "Current Score: " + _scoreManager.CurrentScore.ToString();
        _targetScore.text = "Goal:" + _scoreManager.CurrentLevelTargetScore.ToString();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
    
    }
    public void SetRootElement(UIManager uIManager,VisualElement activeUIRoot)
    {   _uiManager = uIManager;
        _root = activeUIRoot;
    }

    private void InitElements()
    {
        _levelNumber = _root.Q<Label>("LevelNumber");
        _targetScore = _root.Q<Label>("TargetScore");
        _currentScore = _root.Q<Label>("CurrentScore");
        _startButton = _root.Q<Button>("Start");

        _startButton.clicked += StartButtonClicked;
    }



    private void StartButtonClicked()
    {   
        GameManager.Instance.playingGame = true;
        _uiManager.DisplayUI(UIManager.e_UiDocuments.GameUI);
        _playerManager.GetPlayer().ApplyMovementSpeedDebuff();
    }
}