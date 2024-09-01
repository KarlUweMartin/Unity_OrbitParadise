using UnityEngine;

public class GravityObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Orbiter>(out var orbiter)) 
        {
            orbiter.DestroyOrbiter();
        }
    }
}
