using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour
{
    private bool dragging = false;
    private Vector3 offset;
    CameraMovement cm;

    private void Start()
    {
        cm = Camera.main.GetComponent<CameraMovement>();
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        cm.dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        cm.dragging = false;
    }

    private void Update()
    {
        if (dragging)
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
    }
}
