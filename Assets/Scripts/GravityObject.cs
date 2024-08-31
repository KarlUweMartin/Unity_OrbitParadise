using System.Collections.Generic;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public float Gravity = 1;

    void AddOrbiter() 
    {
        if(_orbiters == null) 
        {
            _orbiters = new List<GameObject>();
        }

        var sphere = Instantiate(_orbiter).GetComponent<Orbiter>();
        sphere.center = this;
        sphere.Randomize();

        _orbiters.Add(sphere.gameObject);
    }

    public void GetOrbiter(GameObject orbiter)
    {
        _orbiters.Add(orbiter);
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

        if (_orbiters == null) return;
        if (Time.frameCount % 25 == 0) 
        { 
            GameObject it = null;
            foreach (var o in _orbiters)
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
        _orbiters.Remove(it);
        Destroy(it);        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Kill(collision.gameObject);
    }

    [SerializeField] private GameObject _orbiter;
    private List<GameObject> _orbiters = new List<GameObject>();
}
