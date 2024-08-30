using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject template;
    [SerializeField] private GravityObject gravityObject;

    private void Start()
    {
        FindObjectOfType<VirtualJoystick>().OnRelease.AddListener(Launch);
    }

    void Launch(Vector3 position, Vector3 direction, float velocity, float mass) 
    {
        var orbiter = Instantiate(template).GetComponent<Orbiter>();
        orbiter.StartPosition = position;
        orbiter.StartRotation = direction;
        orbiter.Mass = mass;
        orbiter.Velocity = velocity;
        orbiter.center = gravityObject;
        gravityObject.GetOrbiter(orbiter.gameObject);
    }
}
