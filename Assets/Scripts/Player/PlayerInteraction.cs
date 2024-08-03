using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f;
    public LayerMask interactableLayer;
    private Camera playerCamera;
    private CraneControl craneControl;
    private HookControl hookControl;

    private bool isControllingCrane = false;
    private bool isControllingHook = false;

    void Start()
    {
        playerCamera = Camera.main;
        craneControl = FindObjectOfType<CraneControl>();
        hookControl = FindObjectOfType<HookControl>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction();
        }

        if (isControllingCrane)
        {
            craneControl.HandleCraneInput();
        }

        if (isControllingHook)
        {
            hookControl.HandleHookInput();
        }
    }

    void HandleInteraction()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableLayer))
        {
            IntreactableObj interactable = hit.collider.GetComponent<IntreactableObj>();
            if (interactable != null)
            {
                
            }
        }
    }
}
