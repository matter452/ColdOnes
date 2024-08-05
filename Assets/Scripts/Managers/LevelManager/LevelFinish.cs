using UnityEngine;

public class LevelFinish : MonoBehaviour 
{   
    private void Awake()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            GameManager.GetUIManager().DisplayUI(UIManager.e_UiDocuments.ScoreUI);
        }
    }
}