using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private void Start()
    {
        _openToggle.onValueChanged.AddListener(Open);
        Open(false);
    }

    private void Open(bool open)
    {
        if (_rect == null) 
        {
            _rect = GetComponent<RectTransform>();
        }

        _rect.anchoredPosition = new Vector2(open ? 0 : -150, 15);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Models.TouchingUi = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Models.TouchingUi = false;
    }

    [SerializeField] private Toggle _openToggle;
    private RectTransform _rect;
}
