using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class IntreactableObj : MonoBehaviour, IInteractable
{

    public String ToolTipMessage;
    public GameObject tooltipPrefab; // Reference to the tooltip prefab
    private GameObject currentTooltip; // Instance of the current tooltip
    public Transform SourceInteraction;
    public GameObject AffectedObject;
    private Vector3 toolTipOffset = new Vector3(0, 2, 0);
    public float InteractRange;
 
void Start()
{
 
}

void Update()
    {
        // Check if player is within display distance
        if (Input.GetKeyDown(KeyCode.E)){
            Ray r = new Ray(SourceInteraction.position, SourceInteraction.forward);
            if(Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)){
                if(hitInfo.collider.gameObject.TryGetComponent(out IntreactableObj interactObj))
                {
                    interactObj.Interact();
                }
            }
        }
    }

    // Optional: Handle interaction with the object
    public void Interact()
    {
        // Logic for interacting with the object (e.g., picking up, activating, etc.)
        Debug.Log("Interacting with " + gameObject.name);
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player"))
        {
            ShowTooltip();
        }
    }

     void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Destroy the current tooltip when exiting the trigger
            Destroy(currentTooltip);
        }
    }

    public void ShowTooltip()
    {
        currentTooltip = Instantiate(tooltipPrefab, gameObject.transform.position, Quaternion.identity);
        TextMeshPro tipText = currentTooltip.GetComponentInChildren<TextMeshPro>();
        tipText.text = ToolTipMessage;
        currentTooltip.transform.SetParent(transform); // Attach to the interactable object
        currentTooltip.transform.localPosition = toolTipOffset;
    }
}