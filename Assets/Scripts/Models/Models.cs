using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class Models
{
    private static float _orbitCameraDistance = 20f;
    public static float OrbitCameraDistance
    {
        get => _orbitCameraDistance;
        set
        {
            value = Mathf.Clamp(value, 5f, 35f);
            if (value != _orbitCameraDistance)
            {
                var cam = Camera.main;
                cam.transform.localPosition = new Vector3(0,0, -value);
                cam.fieldOfView = Utils.RemapRange(value, 5, 35, 75, 90);
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

    public static bool TouchingUi { get; set; } = false;
}

public static class Utils
{
    public static float RemapRange(float value, float A, float B, float X, float Y)
    {
        return (value - A) / (B - A) * (Y - X) + X;
    }
}