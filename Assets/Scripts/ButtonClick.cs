using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Location location;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            location.Reveal();
        } 
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            location.Hide();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

}
