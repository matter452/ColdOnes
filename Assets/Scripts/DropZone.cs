using UnityEngine;

public class DropZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "ice-chest")
        {
            // Logic for when the object is in the drop zone
            Debug.Log("Level complete!");
        }
    }
}
