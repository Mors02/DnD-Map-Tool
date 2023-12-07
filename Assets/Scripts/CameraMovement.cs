using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 dragOrigin;

    public bool dragging = false;

    [SerializeField]
    private float maxWidth = 15, maxHeight = 10, zoomStep = .1f, minCamSize, maxCamSize;


    // Start is called before the first frame update
    private void Update()
    {
        PanCamera();
        Zoom();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(0) && !dragging)
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0) && !dragging)
        {
            Vector3 diff = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position += diff;
            cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, -maxWidth, maxWidth), Mathf.Clamp(cam.transform.position.y, -maxHeight, maxHeight), -10);
        }
    }

    private void Zoom()
    {

        float scroll, dir = 0;
        scroll = Input.mouseScrollDelta.y;
        if (scroll != 0)
            dir = Mathf.Sign(scroll);

        float newSize = cam.orthographicSize + (zoomStep * -dir);

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);
    }


}
