using UnityEngine;
using UnityEngine.UI;

public class GravityObject : MonoBehaviour
{
    public Gradient Gradient;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _meshRenderer.material = new Material(_meshRenderer.material);
        _gravitySlider.onValueChanged.AddListener((gravity) => Models.Gravity = gravity);

        Models.OnGravityChanged.AddListener((g) => {
            AdjustColorToGravity(g);
            AdjustScaleToGravity(g);
        });
        AdjustColorToGravity(Models.Gravity);
        AdjustScaleToGravity(Models.Gravity);
    }

    private void AdjustColorToGravity(float gravity)
    {
        var gravityNormalized = Utils.RemapRange(gravity, 1f, 25f, 0f, 1f);

        var col = Gradient.Evaluate(gravityNormalized) * 1.5f;
        _meshRenderer.material.SetColor("_EmissionColor", col);
        _gravitySliderThumb.color = col;
    }

    private void AdjustScaleToGravity(float gravity)
    {
        _meshRenderer.transform.localScale = Vector3.one * Utils.RemapRange(gravity, 1f, 25f, 1.4f, .8f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Orbiter>(out var orbiter)) 
        {
            orbiter.DestroyOrbiter();
        }
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            Ray ray;

            if (Input.touchCount > 0) 
            {
                ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            }
            else
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Collider clickedCollider = hit.collider;
                _gravitySlider.gameObject.SetActive(!_gravitySlider.gameObject.activeSelf);
            }
        }
    }

    private MeshRenderer _meshRenderer;
    [SerializeField] private Slider _gravitySlider;
    [SerializeField] private Image _gravitySliderThumb;
}
