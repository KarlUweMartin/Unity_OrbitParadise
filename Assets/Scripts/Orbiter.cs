using UnityEngine;

public class Orbiter : MonoBehaviour
{
    [HideInInspector] public GravityObject GravitiObject;
    public Vector3 StartPosition, StartRotation;
    public int Mass;
    public float Velocity;

    private void Start()
    {
        var gradient = new Gradient();
        GradientColorKey[] colorKey;
        GradientAlphaKey[] alphaKey;

        colorKey = new GradientColorKey[2];
        colorKey[0].color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        colorKey[0].time = 0.0f;
        colorKey[1].color = Color.white;
        colorKey[1].time = 1.0f;

        alphaKey = new GradientAlphaKey[2];
        alphaKey[0].alpha = 1.0f;
        alphaKey[0].time = 0.0f;
        alphaKey[1].alpha = 0.0f;
        alphaKey[1].time = 1.0f;

        gradient.SetKeys(colorKey, alphaKey);

        GetComponent<TrailRenderer>().colorGradient = gradient;

        transform.position = StartPosition;
        transform.eulerAngles = StartRotation;
        transform.localScale = Vector3.one * Utils.RemapRange(Mass, 0, 2000, .12f, .5f);
        _rig.mass = Mass;
        _rig.AddForce(_rig.transform.forward * Velocity, ForceMode.Impulse);

        if (_audio) 
        {
            var sound = GetComponent<OrbiterSound>();
            int octave = 4;
            var note = string.Empty;
            int step = 50;

            if (Mass > 600)
            {
                octave = 3;
            }
   
            int index = (Mass > 50) ? (Mass - 1) / step : 0;
            index = (Mass > 600) ? index - 12 : index;
 
            switch (index)
            {
                case 0: note = "B"; break;
                case 1: note = "A"; break;
                case 2: note = "G"; break;
                case 3: note = "F"; break;
                case 4: note = "E"; break;
                case 5: note = "D"; break;
                case 6: note = "C"; break;
                default: note = "C"; break; 
            }

            sound.SetNoteAndOctave(note , octave);
        }

    }

    private void Update()
    {
        if (Time.frameCount % 100 == 0)
        {
            if (Vector3.Distance(transform.position, GravitiObject.transform.position) > 350)
            {
                DestroyOrbiter();
            }
        }
    }

    private void FixedUpdate()
    {
        var difference = GravitiObject.transform.position - transform.position;
        var dist = difference.magnitude;
        var gravDirection = difference.normalized;
        var grav = Models.Gravity * _rig.mass / (dist * dist);
        var gravVector = gravDirection * grav;

        _rig.AddForce(gravVector, ForceMode.Acceleration);
    }

    public void DestroyOrbiter() 
    {
        Destroy(gameObject);
    }

    public void Randomize()
    {
        StartRotation = new Vector3(Random.Range(0, 359), Random.Range(0, 359), Random.Range(0, 359));
        Mass = Random.Range(15, 50);
        Velocity = Random.Range(15, 50);
        StartPosition = GravitiObject.transform.position + new Vector3(Random.Range(-12, 12), Random.Range(-12, 12), Random.Range(-12, 12));
    }

    [SerializeField] private Rigidbody _rig;
    [SerializeField] private bool _audio = false;
    [SerializeField] private int _maxDistance = 500;

}
