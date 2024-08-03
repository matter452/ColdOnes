using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookControl : MonoBehaviour
{
    public Transform craneHook;
    public float hookMoveSpeed = 5f;

    public void HandleHookInput()
    {
        bool isMovingHookUp = Input.GetKey(KeyCode.W);
        bool isMovingHookDown = Input.GetKey(KeyCode.S);

        MoveCraneHook(isMovingHookUp, isMovingHookDown);
    }

    void MoveCraneHook(bool isMovingHookUp, bool isMovingHookDown)
    {
        if (isMovingHookUp)
        {
            craneHook.localPosition += Vector3.down * hookMoveSpeed * Time.deltaTime;
        }
        else if (isMovingHookDown)
        {
            craneHook.localPosition += Vector3.up * hookMoveSpeed * Time.deltaTime;
        }
    }
}