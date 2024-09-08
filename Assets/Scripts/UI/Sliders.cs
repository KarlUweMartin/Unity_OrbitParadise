using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{
    private void Awake()
    {
        _gravitySlider.onValueChanged.AddListener((gravity) => Models.Gravity = gravity);
        _camOrbitDistanceSlider.onValueChanged.AddListener((dstance) => Models.OrbitCameraDistance = dstance);
    }

    [SerializeField] private Slider _gravitySlider, _camOrbitDistanceSlider;
}
