using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InteractableTip : MonoBehaviour
{
    public Transform targetTransform; // The transform of the interactable object
    public Camera mainCamera; // Reference to the main camera
    public Text tipText; // Reference to the UI Text element for the tip

    void Update()
    {
        if (targetTransform != null)
        {
            // Ensure the tip UI follows the position of the interactable object
            Vector3 screenPos = mainCamera.WorldToScreenPoint(targetTransform.position);
            tipText.rectTransform.position = screenPos;

            // Make the tip always face the camera (billboarding)
            tipText.rectTransform.forward = mainCamera.transform.forward;

            // Show or hide the tip based on the distance from the player
            float distanceToPlayer = Vector3.Distance(targetTransform.position, mainCamera.transform.position);
            tipText.enabled = distanceToPlayer <= 10f; // Adjust the distance threshold as needed
        }
    }
}