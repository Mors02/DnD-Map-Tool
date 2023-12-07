using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCommands : MonoBehaviour
{

    public bool dragging = false;
    // Start is called before the first frame update
    Camera cam;

    GameObject screen;

    GameObject locationList;

    private void Start()
    {
        cam = Camera.main;

        screen = GameObject.FindGameObjectWithTag("DMScreen");
        if (screen != null)
            screen.SetActive(false);

        this.locationList = GameObject.FindGameObjectWithTag("LocationList");

    }

    private void Update()
    {
        if (dragging)
            return;

        //Spawn location
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            GameObject loc = Instantiate(GameAssets.i.locationPrefab, pos, Quaternion.identity);
            loc.transform.SetParent(locationList.transform);

        }

        //show dm screen
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            if (screen != null)
                screen.SetActive(!screen.activeSelf);
        }

        //save
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            locationList.GetComponent<SavingSystem>().Save();
        }
    }
}
