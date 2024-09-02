using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public Vector2 InputVector { get; private set; } = Vector2.zero;
    public bool InputDown { get; private set; } = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / _joystickBackground.sizeDelta.x);
            pos.y = (pos.y / _joystickBackground.sizeDelta.y);

            InputVector = new Vector2(pos.x * 2, pos.y * 2);
            InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;

            _joystickKnob.anchoredPosition = new Vector2(InputVector.x * (_joystickBackground.sizeDelta.x / 2), InputVector.y * (_joystickBackground.sizeDelta.y / 2));

            InputDown = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputVector = Vector2.zero;
        _joystickKnob.anchoredPosition = Vector2.zero;
        InputDown = false;
    }

    [SerializeField] private RectTransform _joystickBackground;
    [SerializeField] private RectTransform _joystickKnob;
}
