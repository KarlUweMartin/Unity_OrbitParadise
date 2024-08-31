using UnityEngine;

public class Orbiter : MonoBehaviour
{
    public float smoothSpeed = 25;
    [SerializeField] Rigidbody rig;
    [HideInInspector] public GravityObject center;

    public Vector3 StartPosition, StartRotation;
    public float Mass;
    public float Velocity;

    private void Start()
    {
        Gradient gradient = new Gradient();
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        // Populate the color keys at the relative time 0 and 1 (0 and 100%)
        colorKey = new GradientColorKey[2];
        colorKey[0].color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.white;
        colorKey[1].time = 1.0f;

        // Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        GetComponent<TrailRenderer>().colorGradient = gradient;

        transform.position = StartPosition;
        transform.eulerAngles = StartRotation;
        rig.mass = Mass;
        transform.localScale = Vector3.one * Mass / 1000;
        rig.AddForce(rig.transform.forward * Velocity, ForceMode.Impulse);
    }

    public void Randomize()
    {
        StartRotation = new Vector3(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
        Mass = Random.Range(15, 50);
        Velocity = Random.Range(15, 50);
        StartPosition = center.transform.position + new Vector3(Random.Range(-12, 12), Random.Range(-12, 12), Random.Range(-12, 12));
    }

    public void SetValues(float newMass, float newVelocity, Vector3 startPos, Vector3 startRot) 
    {
        StartRotation = startRot;
        StartPosition = startPos;
        Mass = newMass;
        Velocity = newVelocity;       
    }

    public void DebugValues() 
    {
        Debug.Log("Position: "+StartPosition);
        Debug.Log("Rotation: "+StartRotation);
        Debug.Log("Mass: "+Mass);
        Debug.Log("Velocity: "+Velocity);
    }

    void FixedUpdate()
    {
        Vector3 difference = center.transform.position - transform.position;

        float dist = difference.magnitude;
        Vector3 gravDirection = difference.normalized;
        float grav = center.Gravity * rig.mass / (dist * dist);
        Vector3 gravVector = gravDirection * grav;
       
        rig.AddForce(gravVector, ForceMode.Acceleration);

       /* if (rig.velocity != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation((transform.position + GetComponent<Rigidbody>().velocity) - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation,  Time.deltaTime * smoothSpeed);
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }*/
        
    }
}
