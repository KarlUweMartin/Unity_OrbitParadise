using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UiPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerUp(PointerEventData eventData)
    {
        Models.TouchingUi = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Models.TouchingUi = false;
    }

    private RectTransform _rect;
}
