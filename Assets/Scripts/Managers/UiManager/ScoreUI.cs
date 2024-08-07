using UnityEngine;
using UnityEngine.UIElements;

public class ScoreUI : MonoBehaviour
{
    private UIManager _uiManager;
    private VisualElement _root;
    private Label _coldOnes;
    private Label _warmOnes;
    private Label _totalScore;
    private Button _myNotBadButton;
    private LevelManager _levelManager;

    void Start()
    {
        _levelManager = GameManager.GetLevelManager();
        InitElements();
        SubscribeToLevelManagerEvents();
    }

    public void SetRootElement(UIManager uIManager, VisualElement activeUIRoot)
    {
        _uiManager = uIManager;
        _root = activeUIRoot;
    }

    private void InitElements()
    {
        _coldOnes = _root.Q<Label>("coldOnesScore");
        _warmOnes = _root.Q<Label>("warmOnesScore");
        _totalScore = _root.Q<Label>("totalScore");
        _myNotBadButton = _root.Q<Button>("scoreButton");
        _myNotBadButton.clicked += buttonClicked;

        UpdateColdOnes(_levelManager.ColdOnes);
        UpdateWarmOnes(_levelManager.WarmOnes);
        UpdateTotalScore(_levelManager.CurrentScore);
    }

    private void buttonClicked()
    {   
        _levelManager.ScoreButtonClicked();
    }

    private void SubscribeToLevelManagerEvents()
    {
        _levelManager.onColdsChanged += UpdateColdOnes;
        _levelManager.onWarmsChanged += UpdateWarmOnes;
        _levelManager.onScoreChanged += UpdateTotalScore;
    }

    private void UnsubscribeFromLevelManagerEvents()
    {
        _levelManager.onColdsChanged -= UpdateColdOnes;
        _levelManager.onWarmsChanged -= UpdateWarmOnes;
        _levelManager.onScoreChanged -= UpdateTotalScore;
    }

    private void OnDestroy()
    {
        UnsubscribeFromLevelManagerEvents();
    }

    private void UpdateColdOnes(int coldOnes)
    {
        _coldOnes.text = "Cold Ones: " + coldOnes.ToString() + "x20";
    }

    private void UpdateWarmOnes(int warmOnes)
    {
        _warmOnes.text = "Warm Ones: " + warmOnes.ToString() + "x2";
    }

    private void UpdateTotalScore(int totalScore)
    {
        _totalScore.text = "Total Score: " + totalScore.ToString();
    }
}
