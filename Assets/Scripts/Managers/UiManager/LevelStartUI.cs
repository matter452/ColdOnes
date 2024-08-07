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
    private LevelManager _levelManager;

    void Start()
    {   
        GameManager.Instance.playingGame = false;
        InitElements();
        UpdateUI();
    }

    private void UpdateUI()
    {
        // Check if LevelManager is assigned
        if (_levelManager != null)
        {
           /*  _levelNumber.text = "Level " + _levelManager.CurrentLevel.ToString();
            _currentScore.text = "Current Score: " + _levelManager.CurrentScore.ToString();
            _targetScore.text = "Goal: " + _levelManager.CurrentLevelTargetScore.ToString(); */
        }
        else
        {
            Debug.LogError("LevelManager is not assigned.");
        }
    }

    public void SetRootElement(UIManager uIManager, VisualElement activeUIRoot)
    {
        _uiManager = uIManager;
        _root = activeUIRoot;
    }

    private void InitElements()
    {
        _levelManager = GameManager.GetLevelManager();
        if (_root == null)
        {
            Debug.LogError("_root is not assigned.");
            return;
        }

        _levelNumber = _root.Q<Label>("LevelNumber");
        _targetScore = _root.Q<Label>("TargetScore");
        _currentScore = _root.Q<Label>("CurrentScore");
        _startButton = _root.Q<Button>("Start");

        if (_startButton != null)
        {
            _startButton.clicked += StartButtonClicked;
        }
        else
        {
            Debug.LogError("Start button not found.");
        }
    }

    private void StartButtonClicked()
    {
        GameManager.Instance.playingGame = true;
        _uiManager.DisplayUI(UIManager.e_UiDocuments.GameUI);
    }
}
