using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField] private GameObject template;
    [SerializeField] private GravityObject gravityObject;
    [SerializeField] private TMP_InputField posX, posY, posZ, rotX, rotY, rotZ, mass, velocity;
    [SerializeField] private Button button;

    [SerializeField] private Transform spawn;
    [SerializeField] private Slider spawnRadius;

    private void Awake()
    {
        button.onClick.AddListener(() => Launch());
        spawnRadius.onValueChanged.AddListener((v) => spawn.position = new Vector3(v, spawn.position.y, spawn.position.z));
    }

    private void Bam(Vector3 position, Vector3 direction, float velocity)
    {
        Debug.Log(direction);

        var orbiter = Instantiate(template).GetComponent<Orbiter>();
        //orbiter.startPosition = new Vector3(StringToInt(posX.text), StringToInt(posY.text), StringToInt(posZ.text));
        orbiter.StartPosition = position;
        orbiter.StartRotation = direction;
        orbiter.Mass = StringToInt(mass.text);
        orbiter.Velocity = velocity / 5;
        orbiter.center = gravityObject;
        gravityObject.GetOrbiter(orbiter.gameObject);
    }

    private void Launch()
    {
        var orbiter = Instantiate(template).GetComponent<Orbiter>();
        //orbiter.startPosition = new Vector3(StringToInt(posX.text), StringToInt(posY.text), StringToInt(posZ.text));
        orbiter.StartPosition = spawn.position;
        orbiter.StartRotation = spawn.eulerAngles;
        orbiter.Mass = StringToInt(mass.text);
        orbiter.Velocity = StringToInt(velocity.text);
        orbiter.center = gravityObject;
        gravityObject.GetOrbiter(orbiter.gameObject);
    }

    private void Launch(Vector3 pos, Vector3 direction, float velocity)
    {
      
    }


    int StringToInt(string tanga) 
    {       
        if (int.TryParse(tanga, out int result))
            return result;
         else 
            return 0;
    }
}
