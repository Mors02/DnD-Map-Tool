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

    ProjectManager pm;

    PopupWindow popup;

    ChangeBackground cb;

    private void Start()
    {
        cam = Camera.main;

        screen = GameObject.FindGameObjectWithTag("DMScreen");
        if (screen != null)
            screen.SetActive(false);

        this.locationList = GameObject.FindGameObjectWithTag("LocationList");
        this.popup = GameObject.Find("UI").GetComponent<PopupWindow>();
        this.pm = GameObject.FindGameObjectWithTag("ProjectSelect").GetComponent<ProjectManager>();
        this.cb = GameObject.Find("background").GetComponent<ChangeBackground>();
    }

    private void Update()
    {
        if (dragging)
            return;

        //Spawn location
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.E))
        {
            Vector2 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            this.Spawn(pos);
        }

        //show dm screen
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.D))
        {
            this.Screen();
        }

        //save
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S))
        {
            this.Save();
        }

        //load
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.L))
        {
            this.Load();
        }


        //change background
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Q))
        {
            this.ChangeBackground();
        }

    }

    public void Load()
    {
        this.pm.Show();
    }

    public void LoadProject(Project proj)
    {
        OperationResult op = locationList.GetComponent<SavingSystem>().Load(proj.dirName);
        popup.AddToQueue(op.message);
    }

    public OperationResult Save()
    {
        OperationResult op = locationList.GetComponent<SavingSystem>().Save();
        popup.AddToQueue(op.message);
        return op;
    }

    public void ChangeBackground()
    {
        cb.OpenExplorer();
    }

    public void Screen()
    {
        if (screen != null)
            screen.SetActive(!screen.activeSelf);
    }

    public void Spawn(Vector2 pos)
    {
        GameObject loc = Instantiate(GameAssets.i.locationPrefab, pos, Quaternion.identity);
        loc.transform.SetParent(locationList.transform);
    }
    
}
