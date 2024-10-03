using UnityEngine;

public class RotateWithAccelerationAndMomentum : MonoBehaviour
{
    void Update()
    {
        if (_joystick.InputDown)
        {
            _inertiaDirection = new Vector3(-_joystick.InputVector.y, _joystick.InputVector.x, 0);  
            _currentRotationSpeed += _joystick.InputVector.magnitude;
            _currentRotationSpeed = Mathf.Min(_currentRotationSpeed, _maxRotationSpeed * _joystick.InputVector.magnitude);
        }
        else
        {
            if (_joystick.InputVector == Vector2.zero)
            {
                if (_currentRotationSpeed > 0)
                {
                    _currentRotationSpeed -= _deceleration * Time.deltaTime;
                    _currentRotationSpeed = Mathf.Max(_currentRotationSpeed, 0);
                }
            }
        }

        transform.Rotate(_inertiaDirection, _currentRotationSpeed * Time.deltaTime);
    }

    [SerializeField] private VirtualJoystick _joystick;
    [SerializeField] private float _maxRotationSpeed = 200f;
    [SerializeField] private float _aacceleration = 50f;
    [SerializeField] private float _deceleration = 30f;
    private float _currentRotationSpeed = 0f;
    private Vector3 _inertiaDirection = Vector3.zero;
}

