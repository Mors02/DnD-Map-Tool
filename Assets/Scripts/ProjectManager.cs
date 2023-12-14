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

    public int projNum = 0, lastSelected = -1;

    [SerializeField]
    Vector2 openPosition = new Vector2(3, -20);

    [SerializeField]
    Vector2 closePosition = new Vector2(3, 400);

    [SerializeField]
    GameObject newProj;

    [SerializeField]
    TMP_InputField dirName;

    [SerializeField]
    Button deleteButton, loadButton;

    CameraMovement cm;
    KeyboardCommands sl;

    // Start is called before the first frame update
    void Start()
    {
        this.rect = gameObject.GetComponent<RectTransform>();
        cm = Camera.main.GetComponent<CameraMovement>();
        sl = Camera.main.GetComponent<KeyboardCommands>();

        this.Close();

        this.RetrieveProjects();

        this.UndoNewProject();

    }

    public void OpenProject(Project proj, int index)
    {
        proj.IsSelected(true);
        if (lastSelected != -1)
        {
            //deleteButton.interactable = true;
            loadButton.interactable = true;
            projects[lastSelected].IsSelected(false);
        }
        lastSelected = index;

        //Camera.main.GetComponent<KeyboardCommands>().LoadProject(proj);
    }

    public void LoadProject()
    {
        Camera.main.GetComponent<KeyboardCommands>().LoadProject(projects[lastSelected]);
        this.Close();
    }

    public void Show()
    {
        this.rect.anchoredPosition = openPosition;
        cm.dragging = true;
        sl.dragging = true;
    }

    public void Close()
    {
        this.rect.anchoredPosition = closePosition;
        cm.dragging = false;
        sl.dragging = false;
    }

    public void NewProject()
    {
        this.newProj.SetActive(true);
    }

    public void ConfirmProjectName()
    {
        string dirPath = Application.persistentDataPath + "/" + dirName.text;
        dirName.text = "";
        if (Directory.Exists(dirPath))
        {
            Debug.Log(dirName.text + " already exists");
        } else
        {
            Directory.CreateDirectory(dirPath);
            this.RetrieveProjects();
            this.UndoNewProject();
        }
    }

    public void UndoNewProject()
    {
        this.newProj.SetActive(false);
    }

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
                Debug.Log(dir.Name);
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
