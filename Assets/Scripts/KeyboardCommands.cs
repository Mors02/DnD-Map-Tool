using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardCommands : MonoBehaviour
{

    public bool dragging = false;
    // Start is called before the first frame update
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
<<<<<<< Updated upstream

=======
        screen = GameObject.FindGameObjectWithTag("DMScreen");
        if (screen != null)
            screen.SetActive(false);
>>>>>>> Stashed changes
    }

    private void Update()
    {
        if (dragging)
            return;

        //Spawn location
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            Instantiate(GameAssets.i.locationPrefab, pos, Quaternion.identity);
        }

        //show dm screen
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
<<<<<<< Updated upstream

=======
            if (screen != null)
                screen.SetActive(!screen.activeSelf);
>>>>>>> Stashed changes
        }

        //save
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {

        }
    }
}
