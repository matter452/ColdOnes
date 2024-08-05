using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUI : MonoBehaviour
{

    private VisualTreeAsset _ui;
    private UIManager _uiManager;
    private VisualElement _root; 
    private Button _resumeButton;
    private Button _quitButton;
    
    void Start() 
    {
        InitButtons();
    }

    private void InitButtons(){
        _resumeButton = _root.Q<Button>("resumeButton");
        _quitButton = _root.Q<Button>("quitButton");

        _quitButton.clicked += StartButtonCLicked;
        _resumeButton.clicked += ResumeButtonClicked;
    }

    public void SetRootElement(UIManager uIManager,VisualElement activeUIRoot)
    {   _uiManager = uIManager;
        _root = activeUIRoot;
    }
    private void StartButtonCLicked()
    {

    }
    private void ResumeButtonClicked()
    {

    }
}