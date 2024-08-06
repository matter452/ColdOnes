using System.ComponentModel.Design.Serialization;
using System.Xml.Serialization;
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
    private ScoreManager _scoreManager;
    private IceChest _iceChest;

    void Start()
    {
        _scoreManager = GameManager.GetScoreManager();
        _levelManager = GameManager.GetLevelManager();
        _iceChest = GameManager.GetPlayerManager().GetPlayer().GetPlayerIceChest();
        InitElements();
    }

    public void SetRootElement(UIManager uIManager,VisualElement activeUIRoot)
    {   _uiManager = uIManager;
        _root = activeUIRoot;
    }

    private void InitElements()
    {
        _coldOnes = _root.Q<Label>("coldOnes");
        _warmOnes = _root.Q<Label>("warmOnes");
        _totalScore = _root.Q<Label>("totalScore");
        _myNotBadButton = _root.Q<Button>("scoreButton");
        _myNotBadButton.clicked += buttonClicked;

        _coldOnes.text = "Cold Ones: "+_iceChest.GetColdOnes().ToString();
        _warmOnes.text = "Warm Ones: "+_iceChest.GetWarmOnes().ToString();
        _totalScore.text = "Total Score: "+_scoreManager.CurrentScore.ToString();

    }

    private void buttonClicked()
    {  
        _levelManager.ScoreButtonClicked();
    }
}