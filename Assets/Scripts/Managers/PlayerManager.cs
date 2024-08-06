using UnityEngine;
using System;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]private PlayerController playerPrefab;
    private PlayerController _player;
    private IceChest _iceChest;
    public event Action<int> onColdsChanged;
    public event Action<int> onWarmsChanged;
    public int colds;
    public int warms;

    private GameManager _gameManager;

    void Start()
    {
        _gameManager = GameManager.Instance;
    }

    public void SpawnPlayer(Transform spawnLocation)
    {
        _player = Instantiate(playerPrefab, spawnLocation);
        _iceChest = _player.GetPlayerIceChest();
        colds = 0;
        warms = 0;
    }
    public PlayerController GetPlayer()
    {
        return _player;
    }
    private IceChest GetIceChest()
    {
        return _player.GetPlayerIceChest();
    }

    public void GetColdOnes()
    {   
        colds = _iceChest.GetCalculatedColdOnes();
        
    }

    public void GetWarmOnes()
    {
        warms = _iceChest.GetCalculatedWarmOnes();
    }

    public int Warms{
        get => warms;
        set{
            warms = value;
            onWarmsChanged?.Invoke(warms);
        }
    }

    public int Colds{
        get => colds;
        set{
            colds = value;
            onColdsChanged?.Invoke(colds);
        }
    }
}