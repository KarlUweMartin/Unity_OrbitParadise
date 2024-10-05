using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    private void Start()
    {
        _onOffToggle.onValueChanged.AddListener(gameObject.SetActive);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Models.TouchingUi = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Models.TouchingUi = false;
    }

    private RectTransform _rect;
    [SerializeField] private Toggle _onOffToggle;
}
