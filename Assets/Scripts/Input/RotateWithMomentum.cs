using UnityEngine;

public class RotateWithAccelerationAndMomentum : MonoBehaviour
{
    public float maxRotationSpeed = 200f;  // Maximum speed at which the object rotates
    public float acceleration = 50f;       // Acceleration rate when the joystick is moved
    public float deceleration = 30f;       // Deceleration rate when the joystick is released
    private float currentRotationSpeed = 0f;

    private Vector3 _inertiaDirection = Vector3.zero;

    void Update()
    {
        // Get the magnitude of the joystick input (how far it's being pushed)
        float inputMagnitude = _joystick.InputVector.magnitude;

        if (_joystick.InputDown)
        {
            // Set the rotation direction based on the joystick's input vector
            _inertiaDirection = new Vector3(-_joystick.InputVector.y, _joystick.InputVector.x, 0);

            // Accelerate the rotation based on the joystick input magnitude
            currentRotationSpeed += acceleration * inputMagnitude * Time.deltaTime;
            currentRotationSpeed = Mathf.Min(currentRotationSpeed, maxRotationSpeed * inputMagnitude); // Clamp to the maximum speed for the current input
        }
        else
        {
            // Decelerate the rotation when the joystick is released
            if (currentRotationSpeed > 0)
            {
                currentRotationSpeed -= deceleration * Time.deltaTime;
                currentRotationSpeed = Mathf.Max(currentRotationSpeed, 0); // Clamp to zero to stop rotation
            }
        }

        // Apply the rotation to the object
        transform.Rotate(_inertiaDirection, currentRotationSpeed * Time.deltaTime);
    }

    [SerializeField] private VirtualJoystick _joystick;
}
