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

        _quitButton.clicked += QuitButtonCLicked;
        _resumeButton.clicked += ResumeButtonClicked;
    }

    public void SetRootElement(UIManager uIManager,VisualElement activeUIRoot)
    {   _uiManager = uIManager;
        _root = activeUIRoot;
    }
    private void QuitButtonCLicked()
    {
        _uiManager.DisplayUI(UIManager.e_UiDocuments.MainMenuUI);
    }
    private void ResumeButtonClicked()
    {
        GameManager.Instance.playingGame = true;
        _uiManager.DisplayUI(UIManager.e_UiDocuments.GameUI);
    }
}