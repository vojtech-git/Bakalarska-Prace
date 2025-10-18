using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 _moveInput;
    private Vector2 _mousePosition;
    private bool _isAttacking;

    public float speed = 5f;
    public Camera playerCamera;

    private PlayerInput _playerInput;

    void OnEnable()
    {
        _playerInput = GetComponent<PlayerInput>();

        // Subscribe to Move
        _playerInput.actions["Move"].performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInput.actions["Move"].canceled += ctx => _moveInput = Vector2.zero;

        // Subscribe to Look (mouse position)
        _playerInput.actions["Look"].performed += ctx => _mousePosition = ctx.ReadValue<Vector2>();
        _playerInput.actions["Look"].canceled += ctx => _mousePosition = Vector2.zero;
        
        _playerInput.actions["Attack"].performed += ctx => _isAttacking = true;
        _playerInput.actions["Attack"].canceled += ctx => _isAttacking = false;
    }

    void OnDisable()
    {
        _playerInput.actions["Move"].performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
        _playerInput.actions["Move"].canceled -= ctx => _moveInput = Vector2.zero;

        _playerInput.actions["Look"].performed -= ctx => _mousePosition = ctx.ReadValue<Vector2>();
        _playerInput.actions["Look"].canceled -= ctx => _mousePosition = Vector2.zero;
        
        _playerInput.actions["Attack"].performed -= ctx => _isAttacking = true;
        _playerInput.actions["Attack"].canceled -= ctx => _isAttacking = false;
    }

    void Update()
    {
        // Movement
        Vector3 move = new Vector3(_moveInput.x, 0, _moveInput.y);
        transform.Translate(move * (speed * Time.deltaTime), Space.World);

        // Face mouse
        if (_isAttacking && _mousePosition.sqrMagnitude > 0.001f)
        {
            Ray ray = playerCamera.ScreenPointToRay(_mousePosition);
            Plane groundPlane = new Plane(Vector3.up, gameObject.transform.position);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 target = ray.GetPoint(distance);
                Vector3 direction = target - transform.position;
                direction.y = 0;
                if (direction.sqrMagnitude > 0.001f)
                    transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        else if (move.sqrMagnitude > 0.001f)
        {
            // Face movement
            transform.rotation = Quaternion.LookRotation(move);
        }
    }
}
