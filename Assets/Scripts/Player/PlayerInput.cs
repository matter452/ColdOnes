using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public bool jump;
    public bool grab;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");
        grab = Input.GetButtonDown("Fire1"); // Assuming "Fire1" is mapped to the grab action (e.g., left mouse button)
    }
}