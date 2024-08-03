using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerController playerPrefab;
    private PlayerController _player;

    private GameManager _gameManager = GameManager.Instance;

    public void SpawnPlayer(Transform spawnLocation)
    {
        _player = Instantiate(playerPrefab, spawnLocation);
    }

    


}