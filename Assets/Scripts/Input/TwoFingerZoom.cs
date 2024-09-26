using UnityEngine;

public class TwoFingerZoom : MonoBehaviour
{
    public float swipeSensitivity = 0.01f;
    public float mouseSensitivity = 0.1f;

    private Vector2[] lastFingerPositions = new Vector2[2];
    private bool isTwoFingerSwipe = false;
    private bool isRightClickDragging = false;
    private Vector3 lastMousePosition;

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

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                lastFingerPositions[0] = touch0.position;
                lastFingerPositions[1] = touch1.position;
                isTwoFingerSwipe = true;
            }
            else if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
            {
                Vector2 touch0Delta = touch0.position - lastFingerPositions[0];
                Vector2 touch1Delta = touch1.position - lastFingerPositions[1];

                if (Mathf.Abs(touch0Delta.y - touch1Delta.y) < 50f)
                {
                    float averageDeltaY = (touch0Delta.y + touch1Delta.y) / 2f;
                    Models.OrbitCameraDistance += averageDeltaY * swipeSensitivity;
                    Models.OrbitCameraDistance = Mathf.Clamp(Models.OrbitCameraDistance, 5f, 35f);

                    lastFingerPositions[0] = touch0.position;
                    lastFingerPositions[1] = touch1.position;
                }
            }
        }
        else
        {
            isTwoFingerSwipe = false;
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightClickDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1) && isRightClickDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
            Models.OrbitCameraDistance -= mouseDelta.y * mouseSensitivity;
            Models.OrbitCameraDistance = Mathf.Clamp(Models.OrbitCameraDistance, 5f, 35f);
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRightClickDragging = false;
        }
    }
}
