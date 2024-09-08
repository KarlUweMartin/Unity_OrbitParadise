using System;
using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public Gradient Gradient;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material = new Material(_meshRenderer.material);

        Models.OnGravityChanged.AddListener(AdjustColorToGravity);
        AdjustColorToGravity(Models.Gravity);
    }

    private void AdjustColorToGravity(float gravity)
    {
        var gravityNormalized = Utils.RemapRange(gravity, 1f, 25f, 0f, 1f);
        _meshRenderer.material.SetColor("_EmissionColor", Gradient.Evaluate(gravityNormalized) * 1.5f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Orbiter>(out var orbiter)) 
        {
            orbiter.DestroyOrbiter();
        }
    }

    private MeshRenderer _meshRenderer;
}
