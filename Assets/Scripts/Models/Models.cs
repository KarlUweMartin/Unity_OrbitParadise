using UnityEngine;
using UnityEngine.Events;

public static class Models
{
    public static UnityEvent<float> OnOrbitCameraSpeedChanged = new();
    private static float _orbitCameraSpeed = 7.5f;
    public static float OrbitCameraSpeed 
    { 
        get => _orbitCameraSpeed;
        set 
        {
            if (value != _orbitCameraSpeed) 
            {
                OnOrbitCameraSpeedChanged.Invoke(value);
            }

            _orbitCameraSpeed = value;
        }
    }


    private static float _orbitCameraDistance = 20f;
    public static float OrbitCameraDistance
    {
        get => _orbitCameraDistance;
        set
        {
            if (value != _orbitCameraDistance)
            {
                var cam = Camera.main.transform;
                cam.localPosition = new Vector3(0,0, -value);
            }

            _orbitCameraDistance = value;
        }
    }

    public static UnityEvent<float> OnGravityChanged = new();
    private static float _gravity = 15f;
    public static float Gravity
    {
        get => _gravity;
        set
        {
            if (value != _gravity)
            {
                OnGravityChanged.Invoke(value);
            }

            _gravity = value;
        }
    }
}
