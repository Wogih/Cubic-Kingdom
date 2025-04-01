using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private GameObject _cameraPosition;
    [SerializeField] private int _sensitivityX, _sensitivityY;
    [SerializeField] private float _currentVerticalAngle = 90;
    [SerializeField] private int _minZoom, _maxZoom;
    [SerializeField] private int _scrollSpeed;

    private Rigidbody _rb;
    private Camera _camera;

    private void Start()
    {
        _rb = _cameraPosition.GetComponent<Rigidbody>();
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0 )
        {
            Move();
        }

        if (Input.GetMouseButton(2))
        {
            Rotate();
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") != 0)
        {
            ChangeCameraZoom();
        }
    }

    private void ChangeCameraZoom()
    {
        _camera.fieldOfView = Mathf.Clamp(_camera.fieldOfView - Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed, _maxZoom, _minZoom);
    }

    private void Move()
    {
        Vector3 inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        Vector3 moveDirection = _rb.transform.TransformDirection(inputDirection);
        int boost = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        Vector3 newPosition = _rb.position + moveDirection * _moveSpeed * Time.deltaTime * boost;

        newPosition.x = Mathf.Clamp(newPosition.x, -80, +80);
        newPosition.z = Mathf.Clamp(newPosition.z, -80, +80);

        _rb.MovePosition(newPosition);
    }

    private void Rotate()
    {
        Vector3 cameraPosition = _cameraPosition.transform.position;

        float rotationX = Input.GetAxisRaw("Mouse X") * _sensitivityX * Time.deltaTime;
        _cameraPosition.transform.RotateAround(cameraPosition, Vector3.up, rotationX);

        float rotationY = -Input.GetAxisRaw("Mouse Y") * _sensitivityY * Time.deltaTime;
        float newVerticalAngle = _currentVerticalAngle + rotationY;
        newVerticalAngle = Mathf.Clamp(newVerticalAngle, 5f, 90f);

        if (Mathf.Abs(newVerticalAngle - _currentVerticalAngle) > float.Epsilon)
        {
            transform.RotateAround(cameraPosition, transform.right, newVerticalAngle - _currentVerticalAngle);
            _currentVerticalAngle = newVerticalAngle;
        }
    }
}
