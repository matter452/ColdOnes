using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public enum CameraMode { FreeFollow, Isometric }

    public Transform target; 
    public float distance = 10f; 
    public float xSpeed = 20.0f;
    public float ySpeed = 20.0f; 
    public float yMinLimit = -20f; 
    public float yMaxLimit = 80f;
    public float distanceMin = 12f; 
    public float distanceMax = 15f; 
    public CameraMode cameraMode = CameraMode.FreeFollow;
    public Vector3 isometricOffset = new Vector3(20f, 20f, 1f); 
    public float isometricFOV = 30f;
    public float followFOV = 60f;
    private Camera mainCamera;

    private float x = 0.0f; // X rotation
    private float y = 0.0f; // Y rotation

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        mainCamera = GetComponent<Camera>();

        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }

        // Set initial position and rotation
    }

    void LateUpdate()
    {   if(GameManager.Instance.playingGame == false)
    {
        return;
    }
        if (Input.GetKeyDown(KeyCode.C))
        {
            cameraMode = cameraMode == CameraMode.FreeFollow ? CameraMode.Isometric : CameraMode.FreeFollow;
        }

        if (target)
        {
            if (cameraMode == CameraMode.FreeFollow)
            {   mainCamera.fieldOfView = followFOV;
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                Quaternion rotation = Quaternion.Euler(y, x, 0);

                // Perform a line cast to detect collisions
                RaycastHit hit;
                if (Physics.Linecast(target.position, transform.position, out hit))
                {
                    // Adjust distance but ensure it stays within the valid range
                    distance = Mathf.Clamp(hit.distance, distanceMin, distanceMax);
                }

                // Clamp the distance to ensure it doesn't get too close
                distance = Mathf.Clamp(distance, distanceMin, distanceMax);

                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

                transform.rotation = rotation;
                transform.position = position;
            }
            else if (cameraMode == CameraMode.Isometric)
            {
                // Fixed offset for isometric view
                mainCamera.fieldOfView = isometricFOV;
                Vector3 position = target.position + isometricOffset;
                transform.position = position;
                transform.LookAt(target);
            }
        }
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
