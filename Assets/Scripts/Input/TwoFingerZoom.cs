using UnityEngine;

public class TwoFingerZoom : MonoBehaviour
{
    public float swipeSensitivity = 0.01f; // Sensitivity for swipe
    public float mouseSensitivity = 0.1f;  // Sensitivity for mouse input

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
        // Check for two-finger touch
        if (Input.touchCount == 2)
        {
            // Get the touches
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
            {
                // Store the initial positions of the two fingers
                lastFingerPositions[0] = touch0.position;
                lastFingerPositions[1] = touch1.position;
                isTwoFingerSwipe = true;
            }
            else if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
            {
                // Calculate the movement of both fingers
                Vector2 touch0Delta = touch0.position - lastFingerPositions[0];
                Vector2 touch1Delta = touch1.position - lastFingerPositions[1];

                // Check if the fingers are moving in the same direction (Y axis)
                if (Mathf.Abs(touch0Delta.y - touch1Delta.y) < 50f) // Adjust threshold as needed
                {
                    float averageDeltaY = (touch0Delta.y + touch1Delta.y) / 2f;

                    // Adjust the global orbit camera distance based on swipe
                    Models.OrbitCameraDistance += averageDeltaY * swipeSensitivity;

                    // Clamp the value to prevent extreme zoom levels, if needed
                    Models.OrbitCameraDistance = Mathf.Clamp(Models.OrbitCameraDistance, 5f, 35f); // Adjust min/max values as per your needs

                    // Update the last positions
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
        // Check if right mouse button is held down
        if (Input.GetMouseButtonDown(1))
        {
            isRightClickDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        // If right mouse button is held and moving
        if (Input.GetMouseButton(1) && isRightClickDragging)
        {
            Vector3 mouseDelta = Input.mousePosition - lastMousePosition;

            // Adjust the global orbit camera distance based on mouse Y movement
            Models.OrbitCameraDistance -= mouseDelta.y * mouseSensitivity;

            // Clamp the value to prevent extreme zoom levels, if needed
            Models.OrbitCameraDistance = Mathf.Clamp(Models.OrbitCameraDistance, 5f, 35f); // Adjust min/max values as per your needs

            // Update the last mouse position
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            isRightClickDragging = false;
        }
    }
}
