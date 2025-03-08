using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Vector2 InputVector { get; private set; } = Vector2.zero;
    public bool InputDown { get; private set; } = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
        Models.TouchingUi = true;
        _resetButton.gameObject.SetActive(true);
        _knowArrow.enabled = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = pos.x / _joystickBackground.sizeDelta.x;
            pos.y = pos.y / _joystickBackground.sizeDelta.y;

            InputVector = new Vector2(pos.x * 2, pos.y * 2);
            InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;

            _joystickKnob.anchoredPosition = new Vector2(InputVector.x * (_joystickBackground.sizeDelta.x / 2), InputVector.y * (_joystickBackground.sizeDelta.y / 2));

            InputDown = true;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(InputVector.y, InputVector.x) * Mathf.Rad2Deg;

            // Apply rotation to the joystick knob
            _joystickKnob.eulerAngles = new Vector3(0, 0, angle);
        }
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (InputVector != Vector2.zero) 
        {
            _resetButton.onClick.AddListener(ResetToZero);
        }

        InputDown = false;
        Models.TouchingUi = false;
    }

    public void ResetToZero()
    {
        InputVector = Vector2.zero;
        _joystickKnob.anchoredPosition = Vector2.zero;
        _resetButton.gameObject.SetActive(false);
        _knowArrow.enabled = false;
        _resetButton.onClick.RemoveListener(ResetToZero);
    }

    [SerializeField] private RectTransform _joystickBackground;
    [SerializeField] private RectTransform _joystickKnob;
    [SerializeField] private Image _knowArrow;
    [SerializeField] private Button _resetButton;
}
