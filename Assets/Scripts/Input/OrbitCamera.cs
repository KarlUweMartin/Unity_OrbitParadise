using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    public Transform target; // The object to orbit around
    public float orbitSpeed = 10f; // Speed of the orbit
    public float orbitDistance = 5f; // Distance from the target

    void Start()
    {
        if (target != null)
        {
            // Set the initial position at the correct distance
            Vector3 offset = (transform.position - target.position).normalized * orbitDistance;
            transform.position = target.position + offset;
        }
    }

    void Update()
    {
        if (target != null)
        {
            // Keep the object at the correct distance
            Vector3 offset = (transform.position - target.position).normalized * orbitDistance;
            transform.position = target.position + offset;

            // Orbit around the target at the given speed
            transform.RotateAround(target.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
    }
}
