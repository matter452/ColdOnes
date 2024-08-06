using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{   
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private LevelManager _levelManager;
    private UIManager _uiManager;
    private GameObject _player;
    private Transform _playerSpawnPosition;
    public PlayerInput input;
    private Vector3 lastPlayerPosition;
    private ScoreManager _scoreManager;
    public GameObject MainCamera;
    public bool playingGame;
    private static GameManager _instance;
    // Singleton instance
    public static GameManager Instance{
        get{ return _instance; }
    }
     private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject); 
        }
        _playerManager = Instantiate(_playerManager, transform);
        _levelManager = Instantiate(_levelManager, transform);
        _scoreManager = new ScoreManager();
        
    }
    
    
    /* {
        get
        {
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();
                    
                }
            return _instance;
        }
    } */


    void Start()
    {
        _uiManager = UIManager.Instance;

    }

    void Update()
    {
        if(GetPlayerInput().pause)
        {
            _uiManager.DisplayUI(UIManager.e_UiDocuments.PauseMenuUI);
            playingGame = false;
        }
    }

    public void StartGame()
    {   
        _levelManager.StartLevel();
        if(_levelManager.CurrentLevel == 1)
        {
            _playerManager.SpawnPlayer(Instance.transform);
        }
        _uiManager.DisplayUI(UIManager.e_UiDocuments.LevelStartUI);
    }

    public Transform GetInitialSpawn()
    {
        return _playerSpawnPosition;
    }

    public PlayerInput GetPlayerInput()
    {
        return input;
    }

    public void OffMainCamera()
    {
        MainCamera.SetActive(false);
    }

    // Save the player's last known position
    public void SavePlayerPosition(Vector3 position)
    {
        lastPlayerPosition = position;
    }

    // Respawn the player to the last saved position
    public void RespawnPlayer()
    {
        if (_player != null)
        {
            _player.transform.position = lastPlayerPosition;
        }
    }

    // Example method to handle player falling off the map
    public void PlayerFellOffMap()
    {
        RespawnPlayer(); // Respawn player to last position
        // Additional logic such as decrementing lives or showing a message can be added here
    }

    public static LevelManager GetLevelManager()
    {
        return _instance._levelManager;
    }

    public static PlayerManager GetPlayerManager()
    {
        return _instance._playerManager;
    }

    public PlayerController GetPlayer()
    {
        return _playerManager.GetPlayer();
    }

    public static UIManager GetUIManager()
    {
        return _instance._uiManager;
    }
    public static ScoreManager GetScoreManager()
    {
        return _instance._scoreManager;
    }
}
