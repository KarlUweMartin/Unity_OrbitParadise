using UnityEngine;

public class TwoFingerZoom : MonoBehaviour
{
    void Update()
    {
        HandleTouchInput();
        HandleMouseInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            var initialPinchDistance = .0f;
            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                _lastFingerPositions[0] = touch0.position;
                _lastFingerPositions[1] = touch1.position;
                initialPinchDistance = Vector2.Distance(touch0.position, touch1.position);
                _isPinching = true;
            }
            else if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
            {
                var currentPinchDistance = Vector2.Distance(touch0.position, touch1.position);
                var pinchDelta = currentPinchDistance - initialPinchDistance;

                Models.OrbitCameraDistance -= pinchDelta * _pinchSensitivity;

                _lastFingerPositions[0] = touch0.position;
                _lastFingerPositions[1] = touch1.position;
                initialPinchDistance = currentPinchDistance;
            }
        }
        else
        {
            _isPinching = false;
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _isDragging = true;
            _lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1) && _isDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - _lastMousePosition;
            Models.OrbitCameraDistance -= mouseDelta.y * _mouseSensitivity;
            Models.OrbitCameraDistance = Mathf.Clamp(Models.OrbitCameraDistance, 5f, 35f);
            _lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            _isDragging = false;
        }
    }

    [SerializeField] private float _pinchSensitivity = 0.01f;
    [SerializeField] private float _mouseSensitivity = 0.1f;

    private Vector2[] _lastFingerPositions = new Vector2[2];
    private bool _isPinching = false;
    private bool _isDragging = false;
    private Vector3 _lastMousePosition;

}
