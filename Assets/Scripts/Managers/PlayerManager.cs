using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerController playerPrefab;
    private PlayerController _player;
    private IceChest _iceChest;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        _player = Instantiate(playerPrefab, spawnLocation);
        _iceChest = _player.GetPlayerIceChest();
    }

    public PlayerController GetPlayer()
    {
        return _player;
    }

    public IceChest GetIceChest()
    {
        return _iceChest;
    }
}
