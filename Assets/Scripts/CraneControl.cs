using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneControl : MonoBehaviour
{
    public Transform craneHead;
    public Transform craneArm;
    public Transform craneHook;

    public float headRotationSpeed = 30f;
    public float armMoveSpeed = 10f;
    public float hookMoveSpeed = 5f;

    public float armMinHeight = 2f;
    public float armMaxHeight = 10f;

    private float currentArmHeight;
    public PlayerController player;

    // Reference to hook control script or component
    public HookControl hookControl;

    private bool controlsActive = false;

    void Start()
    {
        DeactivateControls();
    }
    
    public void HandleCraneInput()
    {
        bool isRotatingLeft = Input.GetKey(KeyCode.A);
        bool isRotatingRight = Input.GetKey(KeyCode.D);
        bool isMovingArmUp = Input.GetKey(KeyCode.W);
        bool isMovingArmDown = Input.GetKey(KeyCode.S);

        RotateCraneHead(isRotatingLeft, isRotatingRight);
        MoveCraneArm(isMovingArmUp, isMovingArmDown);
    }

    void RotateCraneHead(bool isRotatingLeft, bool isRotatingRight)
    {
        if (isRotatingLeft)
        {
            craneHead.Rotate(Vector3.up, -headRotationSpeed * Time.deltaTime);
        }
        else if (isRotatingRight)
        {
            craneHead.Rotate(Vector3.up, headRotationSpeed * Time.deltaTime);
        }
    }

    void MoveCraneArm(bool isMovingArmUp, bool isMovingArmDown)
    {
        currentArmHeight = craneArm.localPosition.y;

        if (isMovingArmUp && currentArmHeight < armMaxHeight)
        {
            craneArm.localPosition += Vector3.up * armMoveSpeed * Time.deltaTime;
        }
        else if (isMovingArmDown && currentArmHeight > armMinHeight)
        {
            craneArm.localPosition += Vector3.down * armMoveSpeed * Time.deltaTime;
        }

        // Clamp arm height
        craneArm.localPosition = new Vector3(
            craneArm.localPosition.x,
            Mathf.Clamp(craneArm.localPosition.y, armMinHeight, armMaxHeight),
            craneArm.localPosition.z
        );
    }

    public void ActivateControls()
    {
        // Activate crane controls logic here (e.g., enable input handling)
        controlsActive = true;

        // Disable player movement
        if (!player.isInteracting)
        {
            player.isInteracting = true;
        }

        // Disable hook control
        if (hookControl != null)
        {
            hookControl.enabled = false;
        }

        
    }

    public void DeactivateControls()
    {
        // Deactivate crane controls logic here (e.g., disable input handling)
        controlsActive = false;

        // Enable player movement
        if (player.isInteracting)
        {
            player.isInteracting = false;
        }

        // Enable hook control
        if (hookControl != null)
        {
            hookControl.enabled = true;
        }
    }

    public void ToggleControls()
    {
        if (controlsActive)
        {
            DeactivateControls();
        }
        else
        {
            ActivateControls();
        }
    }
}