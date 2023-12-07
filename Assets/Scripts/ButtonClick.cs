using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    Location location;

    UI ui;

    bool dragging = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        Invoke("IsDragging", 0.2f);
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            ui.Open(location);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && !dragging)
        {
            location.Reveal();
        }
        dragging = false;
    }

    public void IsDragging()
    {
        dragging = true;
    }

    public void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }

}
