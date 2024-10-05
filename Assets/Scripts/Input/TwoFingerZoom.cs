using UnityEngine;

public class TwoFingerZoom : MonoBehaviour
{
    void Update()
    {
        if (Input.touchCount == 2)
        {
            HandlePinchInput(Input.GetTouch(0).position, Input.GetTouch(1).position);
        }
        else 
        {
            _lastDist = 0f;
        }

        Models.OrbitCameraDistance += Input.mouseScrollDelta.y;
    }

    void HandlePinchInput(Vector3 touch1, Vector3 touch2)
    {
        var distance = Vector2.Distance(touch1, touch2);
        if (_lastDist != 0) 
        {
            Models.OrbitCameraDistance -= (_lastDist - distance) * _sensitivitiy;
        }

        _lastDist = distance;
    }

    [SerializeField] private float _sensitivitiy = 1;
    private float _lastDist = 0f;
}
