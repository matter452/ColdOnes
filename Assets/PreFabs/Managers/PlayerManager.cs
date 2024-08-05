using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]private PlayerController playerPrefab;
    private PlayerController _player;

    private GameManager _gameManager = GameManager.Instance;

    public void SpawnPlayer(Transform spawnLocation)
    {
        _player = Instantiate(playerPrefab, spawnLocation);
       /*  FollowCamera camera= GetPlayer().GetComponentInChildren<FollowCamera>(); */
        /* camera.MoveToPlayerPosition(); */
    }
    public PlayerController GetPlayer()
    {
        return _player;
    }
    private IceChest GetIceChest()
    {
        return _player.GetPlayerIceChest();
    }

    public int GetColdOnes()
    {
        return GetIceChest().GetColdOnes();
    }

    public int GetWarmOnes()
    {
        return GetIceChest().GetWarmOnes();
    }

    /* public Transform GetCameraStart()
    {
        return _player.cameraStart.transform;
    } */
}