using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private UIManager _uiManager;
    private VisualElement _root;
    private Button _play;
    private Button _quit;

    void Start()
    {
        InitButtons();        
    }
    public void SetRootElement(UIManager uIManager,VisualElement activeUIRoot)
    {   _uiManager = uIManager;
        _root = activeUIRoot;
    }

    private void InitButtons()
    {
        _play = _root.Q<Button>("playGame");
        _quit = _root.Q<Button>("quitGame");

        _play.clicked += PlayButtonCLicked;
        _quit.clicked += QuitButtonClicked;
    }
    private void PlayButtonCLicked()
    {
        GameManager.Instance.StartGame();
    }
    private void QuitButtonClicked()
    {

    }
}