using UnityEngine;

public class GameManager : MonoBehaviour
{   
    private static GameManager _instance;
    // Singleton instance
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                // Find the existing instance in the scene
                _instance = FindObjectOfType<GameManager>();

                // If no instance is found, create a new one
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    _instance = singletonObject.AddComponent<GameManager>();

                    // Optionally make it persist between scenes
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    // Player reference
    private GameObject player;
    private Transform _playerSpawnPosition;
    public PlayerInput input;
    private Vector3 lastPlayerPosition;
        
     private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // Optionally make it persist between scenes
        }
        else if (_instance != this)
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    
    public void StartLevel()
    {
        
    }

    public Transform GetInitialSpawn()
    {
        return _playerSpawnPosition;
    }

    public PlayerInput GetPlayerInput()
    {
        return input;
    }

    // Save the player's last known position
    public void SavePlayerPosition(Vector3 position)
    {
        lastPlayerPosition = position;
    }

    // Respawn the player to the last saved position
    public void RespawnPlayer()
    {
        if (player != null)
        {
            player.transform.position = lastPlayerPosition;
        }
    }

    // Example method to handle player falling off the map
    public void PlayerFellOffMap()
    {
        RespawnPlayer(); // Respawn player to last position
        // Additional logic such as decrementing lives or showing a message can be added here
    }
}
