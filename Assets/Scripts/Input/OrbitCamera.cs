using UnityEngine;
using UnityEngine.UI;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; // The object to orbit around


    void Start()
    {

        if (target != null)
        {
            // Set the initial position at the correct distance
            Vector3 offset = (transform.position - target.position).normalized * Models.OrbitCameraDistance;
            transform.position = target.position + offset;
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Keep the object at the correct distance
            Vector3 offset = (transform.position - target.position).normalized * Models.OrbitCameraDistance;
            transform.position = target.position + offset;

            // Orbit around the target at the given speed
            transform.RotateAround(target.position, Vector3.up, Models.OrbitCameraSpeed * Time.deltaTime);
        }
    }

    [SerializeField] private Slider _speedSlider;
}
