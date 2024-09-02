using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    private void Awake()
    {
        _gravitySlider.onValueChanged.AddListener((gravity) => Models.Gravity = gravity);
        _camOrbitDistanceSlider.onValueChanged.AddListener((dstance) => Models.OrbitCameraDistance = dstance);

        _openUiToggle.onValueChanged.AddListener(Show);
    }

    public void Show(bool show)
    {
        _uiCanvas.gameObject.SetActive(show);
    }

    [SerializeField] private Slider _gravitySlider, _camOrbitDistanceSlider;
    [SerializeField] private Toggle _openUiToggle;
    [SerializeField] private RectTransform _uiCanvas;
}
