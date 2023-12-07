using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Location location;

    UI ui;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            location.Reveal();
        } 
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            ui.Open(location);
        }
    }

    public void Start()
    {
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
    }

}
