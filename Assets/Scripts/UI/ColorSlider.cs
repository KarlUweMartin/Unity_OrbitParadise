using UnityEngine;
using UnityEngine.UI;

public class ColorSlider : MonoBehaviour
{
    void Start()
    {
        _originalColor = Color.white;
        Color.RGBToHSV(_originalColor, out _originalHue, out _originalSaturation, out _originalValue);
        _hueSlider.value = _originalHue;
        _hueSlider.onValueChanged.AddListener(ChangeHue);
    }

    void ChangeHue(float newHue)
    {
        Color newColor = Color.HSVToRGB(newHue, _originalSaturation, _originalValue);
        _sliderThumb.color = newColor;
    }

    private Color _originalColor;
    private float _originalHue, _originalSaturation, _originalValue;
    [SerializeField] private Image _sliderThumb;
    [SerializeField] private Slider _hueSlider;
}