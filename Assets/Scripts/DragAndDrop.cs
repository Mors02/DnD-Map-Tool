using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    CameraMovement cm;
    KeyboardCommands sl;

    private void Start()
    {
        cm = Camera.main.GetComponent<CameraMovement>();
        sl = Camera.main.GetComponent<KeyboardCommands>();
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = sl.dragging = cm.dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = cm.dragging = sl.dragging = false;
    }

    private void Update()
    {
        if (dragging)
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    }
}
