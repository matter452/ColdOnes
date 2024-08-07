using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFinish : MonoBehaviour 
{   
    private void Awake()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            /* GameManager.GetUIManager().DisplayUI(UIManager.e_UiDocuments.ScoreUI); */
            GameManager.Instance.playingGame = false;
            UIManager.Instance.DisplayUI(UIManager.e_UiDocuments.LevelStartUI);
            GameManager.Instance.RespawnPlayer();
            GameManager.Instance.StartNextLevel();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}