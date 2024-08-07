using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{   
    public float baseSpeed = 30f;
    public float appliedMoveSpeed = 30f;
    public float jumpForce = 9f;
    public float rotationSpeed = 720f;
    public Transform cameraTransform; // Reference to the main camera transform
    public LayerMask groundLayer; // Layer mask to identify ground
    public float grabRange = 2f; // Range within which the player can grab objects
    public bool isInteracting = false;
    public float pickupRadius = 2f;

    private Vector3 _moveDirection = Vector3.zero;
    private float _gravity = -9.81f; // Gravity constant
    public float gravityMultiplier = 3f;
    private bool _isGrounded;
    public Transform carryPosition; // Position where the player carries the object
    public Transform cameraStart;
    public LayerMask pickupLayer; // Layer mask for pickable objects
    public LayerMask depositLayer;
    private GameObject _carriedObject = null;
    public int characterShimmy;
    public Transform playerTransorm;


    private CharacterController _characterController;
    private Animator _animator;
    private PlayerInput _playerInput;
    private Rigidbody _rb;
    private GameManager _gameController;
    private float _movementSpeedDebuff;
    [SerializeField]private Transform _iceChestSpawn;
    [SerializeField]private IceChest _IceChestPrefab;
    private IceChest _iceChest;
    public List<AudioClip> audioClips;

    public Transform IceChestSpawn { get => _iceChestSpawn; set => _iceChestSpawn = value; }

    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _playerInput = GameManager.Instance.GetPlayerInput();
        _rb = GetComponent<Rigidbody>();
        _gameController = GameManager.Instance;
        _movementSpeedDebuff = 0;
        _iceChest = Instantiate(_IceChestPrefab);
        _iceChest.transform.position = _iceChestSpawn.transform.position;
        playerTransorm = this.transform;
        // Ensure the character starts at ground level
        SetCharacterAtGroundLevel();
        
    }

    void Update()
    {
        if(_gameController.playingGame == false)
        {
            return;
        }
        _isGrounded = _characterController.isGrounded;
        Move();
        Jump();
        ApplyGravity();
        HandlePickup();
        HandleDeposit();
        if (transform.position.y < -10f)
        {
            _gameController.PlayerFellOffMap();
        }
    }

    private void HandlePickup()
    {
        if (_playerInput.grab)
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

    private void HandleDeposit()
    {
        if(_carriedObject == null || _carriedObject.GetComponent<IceChest>())
            {
                return;
            }
        if(_playerInput.deposit)
        {
            if(_carriedObject.CompareTag("Ice"))
            {
            AudioSource.PlayClipAtPoint(audioClips[1], gameObject.transform.position);
             DepositObject(DepositIce);
             return;
            }
            if(_carriedObject.CompareTag("Brews"))
            {   
                AudioSource.PlayClipAtPoint(audioClips[0], gameObject.transform.position);
                DepositObject(DepositBeer);
                return;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    void Move()
    {   
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
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            _animator.SetBool("isRunning", true);
        }
        else
        {
            _moveDirection.x = 0f;
            _moveDirection.z = 0f;

            _animator.SetBool("isRunning", false);
        }

        if (_isGrounded && _moveDirection.y < 0)
        {
            _moveDirection.y = -2f; // stick the character to the ground
        }
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
            _moveDirection.y = -2f; 
        }
  
        _characterController.Move(new Vector3(0, _moveDirection.y, 0) * Time.deltaTime);

        if (_isGrounded)
        {
            _animator.SetBool("isJumping", false);
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
            if(_carriedObject.CompareTag("Brews"))
            {
                _carriedObject.GetComponent<Beer>().PlayResourceSound();

            }
            if(_carriedObject.GetComponent<IceChest>())
            {
                AudioSource.PlayClipAtPoint(audioClips[4], gameObject.transform.position);

            }
            _carriedObject.transform.SetParent(carryPosition);
        }
    }

    void DropObject()
    {
        if (_carriedObject != null)
        {
            if(_carriedObject.CompareTag("Ice"))
            {
            AudioSource.PlayClipAtPoint(audioClips[4], gameObject.transform.position);
            }
            if(_carriedObject.CompareTag("Brews"))
            {   
                AudioSource.PlayClipAtPoint(audioClips[3], gameObject.transform.position);
            }
            _carriedObject.GetComponent<Rigidbody>().isKinematic = false; // Enable physics
            _carriedObject.transform.SetParent(null);
            _carriedObject = null;
        }
    }

    void DepositObject(DepositDelegate deposit)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, depositLayer);

        if(hits.Length > 0)
        {   deposit();
            Resource instance =_carriedObject.GetComponent<Resource>();

            if (instance != null)
        {
            instance.DestroyResource();
        }
            _carriedObject.transform.SetParent(null);
            _carriedObject = null;
        }
    }
    public delegate void DepositDelegate();
    public void DepositBeer()
    {
        _iceChest.AddBeers();
    }

    public void DepositIce ()
    {
        _iceChest.AddIce();
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
            Debug.LogWarning("Ground not found!");
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
        return this._iceChest;
    }

    public void SetTransform(Transform myTransform)
    {
        playerTransorm = myTransform;
    }
}
