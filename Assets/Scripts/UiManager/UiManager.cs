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
    private GameUI gameplayUI;
    private ScoreUI scoreUI;
    private UpgradesUI upgradeUI;
    private LevelStartUI levelStartUI;

    [SerializeField] private List<UiDocuments> ListOfUIs = new List<UiDocuments>();

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        uiDocComponent = gameObject.GetComponent<UIDocument>();
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        
    }

    void DisplayUI(e_UiDocuments document)
    {
        ActiveUI = document;
        uiDocComponent.visualTreeAsset = ListOfUIs[(int)ActiveUI].UXMLDocument;
        
         switch (ActiveUI)
        {
            case e_UiDocuments.MainMenuUI:
                startMenu = new MainMenuUI();
                break;
            case e_UiDocuments.PauseMenuUI:
                pauseMenu = new PauseMenuUI();
                break;
            case e_UuDocuments.GameUI:
                gameplayUI = new GameUI();
                break;
            case e_UiDocuments.ScoreUI:
                scoreUI = new ScoreUI();
                break;
            case e_UiDocuments.UpgradesUI:
                upgradeUI = new UpgradesUI();
                break;
            case e_UiDocuments.LevelStartUI:
                levelStartUI = new LevelStartUI();
                break;
        }
    }
}
   