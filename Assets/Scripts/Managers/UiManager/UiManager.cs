using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class UIManager : MonoBehaviour
{   
    public enum e_UiDocuments
    {
        ScoreUI = 0,
        LevelStartUI = 1,
        MainMenuUI = 2,
        GameUI = 3,
        UpgradesUI = 4,
        PauseMenuUI = 5
    }

    [Serializable]
    public class UiDocuments{
        public string Name;
        public VisualTreeAsset UXMLDocument;
    }

    private UIDocument uiDocComponent;
    public e_UiDocuments ActiveUI {get; private set; }
    private MainMenuUI startMenu;
    private PauseMenuUI pauseMenu;
    private GameplayUI gameplayUI;
    private ScoreUI scoreUI;
    private UpgradesUI upgradeUI;
    private LevelStartUI levelStartUI;
    private static UIManager _instance;
    public static UIManager Instance{
        get{ return _instance;}
    }

    [SerializeField] private List<UiDocuments> ListOfUIs = new List<UiDocuments>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {   _instance = this;
        uiDocComponent = gameObject.GetComponent<UIDocument>();
        
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        DisplayUI(e_UiDocuments.MainMenuUI);
    }

    public void DisplayUI(e_UiDocuments document)
    {
        ActiveUI = document;
        VisualTreeAsset activeUI = ListOfUIs[(int)ActiveUI].UXMLDocument;
        uiDocComponent.visualTreeAsset = activeUI;
        VisualElement activeUIRoot = uiDocComponent.rootVisualElement;
        
         switch (ActiveUI)
        {
            case e_UiDocuments.MainMenuUI:
                startMenu = gameObject.AddComponent<MainMenuUI>();
                startMenu.SetRootElement(this, activeUIRoot);
                break;
            case e_UiDocuments.PauseMenuUI:
                pauseMenu = gameObject.AddComponent<PauseMenuUI>();
                pauseMenu.SetRootElement(this, activeUIRoot);
                break;
            case e_UiDocuments.GameUI:
                gameplayUI = gameObject.AddComponent<GameplayUI>();
                gameplayUI.SetRootElement(this, activeUIRoot);
                break;
            case e_UiDocuments.ScoreUI:
                scoreUI = gameObject.AddComponent<ScoreUI>();
                scoreUI.SetRootElement(this, activeUIRoot);
                break;
            case e_UiDocuments.UpgradesUI:
                upgradeUI = gameObject.AddComponent<UpgradesUI>();
                upgradeUI.SetRootElement(this, activeUIRoot);
                break;
            case e_UiDocuments.LevelStartUI:
                levelStartUI = gameObject.AddComponent<LevelStartUI>();
                levelStartUI.SetRootElement(this, activeUIRoot);
                break;
        }
    }

}
   