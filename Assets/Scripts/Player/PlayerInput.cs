using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public bool jump;
    public bool grab;
    public bool pause;
    public bool deposit;

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        jump = Input.GetButtonDown("Jump");
        grab = Input.GetButtonDown("Fire1");
        deposit = Input.GetKeyDown(KeyCode.E);
        pause = Input.GetKeyDown(KeyCode.Escape);
    }
}