using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    // Start is called before the first frame update
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(GameAssets.i.locationPrefab, pos, Quaternion.identity);
        }
    }
}
