using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    [SerializeField] private GameObject template;
    [SerializeField] private int maxRadius;
    public float gravityForce = 1;

    private List<GameObject> orbiters = new List<GameObject>();

    void AddOrbiter() 
    {
        if(orbiters == null) 
        {
            orbiters = new List<GameObject>();
        }

        var sphere = Instantiate(template).GetComponent<Orbiter>();
        sphere.center = this;
        sphere.Randomize();

        orbiters.Add(sphere.gameObject);
    }

    public void GetOrbiter(GameObject orbiter)
    {
        orbiters.Add(orbiter);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < 10; i++)
            {
                AddOrbiter();
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            AddOrbiter();
        }

        if (orbiters == null) return;
        if (Time.frameCount % 25 == 0) 
        { 
            GameObject it = null;
            foreach (var o in orbiters)
            {
                if (Vector3.Distance(o.transform.position, transform.position) > 50)
                {
                    it = o.gameObject;
                }
            }

            if(it != null)
                Kill(it);
        }        
    }

    void Kill(GameObject it) 
    {
        orbiters.Remove(it);
        Destroy(it);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Kill(collision.gameObject);
    }

}
