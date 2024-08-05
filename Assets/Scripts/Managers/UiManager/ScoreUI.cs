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

    void Start()
    {
        InitElements();
        _levelManager = GameManager.GetLevelManager();
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
    }

    private void buttonClicked()
    {  
        _levelManager.ScoreButtonClicked();
    }
}