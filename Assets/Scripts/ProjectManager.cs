using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProjectManager : MonoBehaviour
{

    [SerializeField]
    GameObject parent;

    private RectTransform rect; 

    Project[] projects;

    public int projNum = 0, lastSelected = -1, openProject = -1;

    [SerializeField]
    Vector2 openPosition = new Vector2(3, -20);

    [SerializeField]
    Vector2 closePosition = new Vector2(3, 400);

    [SerializeField]
    GameObject newProj;

    [SerializeField]
    GameObject confirmProj;

    [SerializeField]
    DriveManager dm;

    [SerializeField]
    TMP_InputField dirName;

    [SerializeField]
    Button deleteButton, loadButton, cancelButton, commandsButton;

    CameraMovement cm;
    KeyboardCommands sl;

    PopupWindow popup;

    // Start is called before the first frame update
    void Start()
    {
        this.rect = gameObject.GetComponent<RectTransform>();
        cm = Camera.main.GetComponent<CameraMovement>();
        sl = Camera.main.GetComponent<KeyboardCommands>();
        this.popup = GameObject.Find("UI").GetComponent<PopupWindow>();

        //this.Close();

        this.RetrieveProjects();

        this.UndoNewProject();

    }

    //this function select the project to open
    public void OpenProject(Project proj, int index)
    {
        proj.IsSelected(true);
        loadButton.interactable = true;
        if (lastSelected != -1)
        {
            //deleteButton.interactable = true;
            projects[lastSelected].IsSelected(false);
        }
        lastSelected = index;

        //Camera.main.GetComponent<KeyboardCommands>().LoadProject(proj);
    }

    //this function opens the confirm panel
    public void OpenConfirm()
    {
        if (openProject != -1)
            this.confirmProj.SetActive(true);
        else
            this.LoadProject();
    }

    //this function closes the confirm panel
    public void CloseConfirm()
    {
        this.confirmProj.GetComponent<ShowLastSave>().UpdateCounter();
        this.confirmProj.SetActive(false);
    }

    //This function loads the selected project and closes all the panels
    public void LoadProject()
    {
        commandsButton.interactable = true;
        Camera.main.GetComponent<KeyboardCommands>().LoadProject(projects[lastSelected]);
        cancelButton.interactable = true;
        this.CloseConfirm();
        this.Close();
        this.openProject = lastSelected;

    }

    //This function saves the current project, loads the selected project and closes all the panels
    public void SaveAndLoadProject()
    {
        KeyboardCommands key = Camera.main.GetComponent<KeyboardCommands>();
        key.Save();
        key.LoadProject(projects[lastSelected]);
        cancelButton.interactable = true;
        this.CloseConfirm();
        this.Close();
        this.openProject = lastSelected;
    }

    //this function opens the load project panel
    public void Show()
    {
        this.rect.anchoredPosition = openPosition;
        cm.dragging = true;
        sl.dragging = true;
    }

    //this function closes the load project panel
    public void Close()
    {
        this.rect.anchoredPosition = closePosition;
        cm.dragging = false;
        sl.dragging = false;
    }

    //this function opens the new project panel
    public void NewProject()
    {
        this.newProj.SetActive(true);
    }

    //this function closes the new project panel and creates the project
    public void ConfirmProjectName()
    {
        string dirPath = Application.persistentDataPath + "/" + dirName.text;
        if (Directory.Exists(dirPath))
        {
            this.popup.AddToQueue("Il progetto " + dirName.text + " esiste gia'.");
        } 
        else
        {
            dirName.text = "";
            Directory.CreateDirectory(dirPath);
            this.RetrieveProjects();
            this.UndoNewProject();
        }
    }

    //this function closes the new project panel
    public void UndoNewProject()
    {
        this.newProj.SetActive(false);
    }

    public void OpenDriveSelect()
    {
        this.Close();
        this.dm.Show();
    }

    //this function retrieves all the projects folders
    public void RetrieveProjects()
    {
        foreach(Transform child in this.parent.transform)
        {
            Destroy(child.gameObject);
        }

        if (Directory.Exists(Application.persistentDataPath))
        {
            string worldsFolder = Application.persistentDataPath;
            projNum = 0;
            DirectoryInfo d = new DirectoryInfo(worldsFolder);
            projects = new Project[d.GetDirectories().Length];
            
            foreach (var dir in d.GetDirectories())
            {
                //Debug.Log(dir.Name);
                GameObject button = Instantiate(GameAssets.i.projectPrefab, parent.transform);
                Project proj = button.GetComponent<Project>();
                proj.SetUp(dir.FullName, dir.Name);
                int index = projNum;
                button.GetComponent<Button>().onClick.AddListener(delegate { OpenProject(proj, index); });
                projects[projNum++] = proj;

                //dir.Name
            }
        }
        else
        {
            File.Create(Application.persistentDataPath);
            return;
        }
    }
}
