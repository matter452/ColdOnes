using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Content;

public class PlayerController : MonoBehaviour
{   
    public float baseSpeed = 15f;
    public float appliedMoveSpeed;
    public float jumpForce = 7f;
    public float rotationSpeed = 720f;
    public Transform cameraTransform; // Reference to the main camera transform
    public Transform handTransform; // Reference to the hand or point where objects will be held
    public LayerMask groundLayer; // Layer mask to identify ground
    public float grabRange = 2f; // Range within which the player can grab objects
    public bool isInteracting = false;
    public float pickupRadius = 2f;

    private Vector3 _moveDirection = Vector3.zero;
    private float _gravity = -9.81f; // Gravity constant
    public float gravityMultiplier = 2f;
    private bool _isGrounded;
    private bool _isGrabbing = false;
    private Rigidbody _grabbedObjectRb = null;
    private FixedJoint _grabJoint = null;
    public Transform carryPosition; // Position where the player carries the object
    public Transform cameraStart;
    public LayerMask pickupLayer; // Layer mask for pickable objects
    private GameObject _carriedObject = null;
    public int characterShimmy;


    private CharacterController _characterController;
    private Animator _animator;
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    private GameManager _gameController;
    private float _movementSpeedDebuff;
    [SerializeField]private Transform _iceChestSpawn;
    [SerializeField]private IceChest _IceChestPrefab;
    private IceChest _iceChest;


    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _playerInput = GameManager.Instance.GetPlayerInput();
        _rb = GetComponent<Rigidbody>();
        _gameController = GameManager.Instance;
        _movementSpeedDebuff = 0;
        _iceChest = Instantiate(_IceChestPrefab);
        _iceChest.transform.position = _iceChestSpawn.transform.position;

        // Ensure the character starts at ground level
        SetCharacterAtGroundLevel();
        
    }

    void Update()
    {
        _isGrounded = _characterController.isGrounded;
        Move();
        Jump();
        ApplyGravity();
        HandleGrab();
        UpdateAnimator();
        if (transform.position.y < -10f) // Adjust the threshold as per your scene's needs
        {
            _gameController.PlayerFellOffMap(); // Call GameController method to handle respawn
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_carriedObject != null)
            {
                DropObject();
            }
            else
            {
                PickUpObject();
            }
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        
    }

    void Move()
    {   
        if(isInteracting)
        {
            return;
        }
        // Get the direction based on input
        Vector3 direction = new Vector3(_playerInput.horizontal, 0f, _playerInput.vertical).normalized;
        
        if (direction.magnitude >= 0.1f)
        {
            Vector3 forward = cameraTransform.forward;
            Vector3 right = cameraTransform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            // Calculate move direction relative to camera
            Vector3 desiredMoveDirection = forward * direction.z + right * direction.x;
            _moveDirection.x = desiredMoveDirection.x * appliedMoveSpeed;
            _moveDirection.z = desiredMoveDirection.z * appliedMoveSpeed;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(desiredMoveDirection);
            // Smoothly rotate towards the target direction
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Set running animation
            _animator.SetBool("isRunning", true);
        }
        else
        {
            // Decelerate smoothly
            _moveDirection.x = 0f;
            _moveDirection.z = 0f;

            // Set running animation
            _animator.SetBool("isRunning", false);
        }

        if (_isGrounded && _moveDirection.y < 0)
        {
            _moveDirection.y = -2f; // Small negative value to stick the character to the ground
        }

        // Apply movement
        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    void Jump()
    {
        if (_isGrounded && _playerInput.jump)
        {
            _moveDirection.y = jumpForce;
            _animator.SetBool("isJumping", true);
        }
    }

    void ApplyGravity()
    {
        if (!_isGrounded)
        {
            _moveDirection.y += _gravity * gravityMultiplier * Time.deltaTime;
        }
        else if (_moveDirection.y < 0)
        {
            _moveDirection.y = -2f; // Reset the downward velocity when grounded
        }

        // Apply vertical movement
        _characterController.Move(new Vector3(0, _moveDirection.y, 0) * Time.deltaTime);

        if (_isGrounded)
        {
            _animator.SetBool("isJumping", false);
        }
    }

    void HandleGrab()
    {
        if (_playerInput.grab)
        {
            if (_isGrabbing)
            {
                ReleaseObject();
            }
            else
            {
                TryGrabObject();
            }
        }
    }

    void TryGrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, grabRange))
        {
            InteractableObject interactableObject = hit.collider.GetComponent<InteractableObject>();
            if (interactableObject != null)
            {
                if (interactableObject.isMovable)
                {
                    _grabbedObjectRb = hit.collider.GetComponent<Rigidbody>();
                    if (_grabbedObjectRb != null)
                    {
                        _grabJoint = gameObject.AddComponent<FixedJoint>();
                        _grabJoint.connectedBody = _grabbedObjectRb;
                        _grabJoint.anchor = handTransform.localPosition;
                        _grabbedObjectRb.transform.SetParent(handTransform);
                        _isGrabbing = true;
                    }
                }
                else
                {
                    // Logic for grabbing immovable objects like walls
                    Debug.Log("Attempting to grab an immovable object.");
                }
            }
        }
    }

    void ReleaseObject()
    {
        if (_grabJoint != null)
        {
            Destroy(_grabJoint);
            _grabbedObjectRb.transform.SetParent(null);
            _grabbedObjectRb = null;
            _isGrabbing = false;
        }
    }

    void PickUpObject()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, pickupRadius, pickupLayer);

        if (hits.Length > 0)
        {
            _carriedObject = hits[0].gameObject;
            _carriedObject.GetComponent<Rigidbody>().isKinematic = true; // Disable physics
            _carriedObject.transform.position = carryPosition.position;
            _carriedObject.transform.SetParent(carryPosition);
        }
    }

    void DropObject()
    {
        if (_carriedObject != null)
        {
            _carriedObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            _carriedObject.transform.SetParent(null);
            _carriedObject = null;
        }
    }
    

    void UpdateAnimator()
    {
        // Update animator parameters if needed
    }

    void SetCharacterAtGroundLevel()
    {
        // Perform a raycast to find the ground level
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out hit, Mathf.Infinity, groundLayer))
        {
            // Adjust the character's position to be at the ground level
            Vector3 adjustedPosition = hit.point;
            adjustedPosition.y += (_characterController.height / 2)+characterShimmy;
            transform.position = adjustedPosition;

            // Log the new position
            Debug.Log("Character positioned at ground level: " + transform.position);
        }
        else
        {
            Debug.LogWarning("Ground not found! Make sure the groundLayer is set correctly.");
        }
    }

    public void SetMovementPenalty()
    {
        _movementSpeedDebuff = _iceChest.GetMovementPenalty();
    }

    public void ApplyMovementSpeedDebuff()
    {
        appliedMoveSpeed = baseSpeed * (1-_movementSpeedDebuff);
    }

    public IceChest GetPlayerIceChest()
    {
        return _iceChest;
    }
}
