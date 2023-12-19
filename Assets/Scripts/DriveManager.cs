using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityGoogleDrive;

public class DriveManager : MonoBehaviour
{

    [SerializeField]
    Button uploadButton, deleteButton, downloadButton, cancelButton;

    [SerializeField]
    Vector2 openPosition = new Vector2(3, 20);

    [SerializeField]
    Vector2 closePosition = new Vector2(3, 700);

    CameraMovement cm;
    KeyboardCommands sl;

    [SerializeField]
    ProjectManager pm;
    PopupWindow popup;

    SavingSystem ss;

    private RectTransform rect;

    private string UploadFilePath;

    #region Create file section;
    private GoogleDriveFiles.CreateRequest createRequest;
    private string result;
    #endregion

    #region List file section;
    private GoogleDriveFiles.ListRequest listRequest;
    private string query = string.Empty;
    private Dictionary<string, string> results;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        this.rect = gameObject.GetComponent<RectTransform>();
        cm = Camera.main.GetComponent<CameraMovement>();
        sl = Camera.main.GetComponent<KeyboardCommands>();
        this.popup = GameObject.Find("UI").GetComponent<PopupWindow>();
        ss = GameObject.Find("LocationList").GetComponent<SavingSystem>();
        this.Close();
        this.List();
    }
    public void OpenProjectSelect()
    {
        this.Close();
        this.pm.Show();
    }
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

    public void Upload()
    {
        if (ss.dirName != "")
        {
            //UploadFilePath = ss.basePath;
            UploadFilePath = ss.backgroundPath;
            var content = File.ReadAllBytes(UploadFilePath);
            var file = new UnityGoogleDrive.Data.File { Name = Path.GetFileName(UploadFilePath), Content = content };
            file.Parents = new List<string> { "appDataFolder" };
            createRequest = GoogleDriveFiles.Create(file);
            createRequest.Fields = new List<string> { "id", "name", "size", "createdTime" };
            this.uploadButton.interactable = false;
            this.downloadButton.interactable = false;
            createRequest.Send().OnDone += PrintResult;
        }
    }

    public void List()
    {
        listRequest = GoogleDriveFiles.List();
        listRequest.Fields = new List<string> { "files(id, name, size)" };
        //listRequest.Q = $"'appDataFolder' in parents and mimeType = 'application/vnd.google-apps.folder' and trashed = false";
        listRequest.Q = "'appDataFolder' in parents";
        //listRequest.Fields = new List<string> { "nextPageToken, files(id, name, size, createdTime)" };
        listRequest.PageSize = 10;
        /*if (!string.IsNullOrEmpty(query))
            listRequest.Q = string.Format("name contains '{0}'", query);*/
        listRequest.Send().OnDone += BuildResults;
    }

    private void PrintResult(UnityGoogleDrive.Data.File file)
    {
        result = string.Format("Name: {0} Size: {1:0.00}MB Created: {2:dd.MM.yyyy HH:MM:ss}\nID: {3}",
            file.Name,
            file.Size * .000001f,
            file.CreatedTime,
            file.Id);
        Debug.Log(result);
        this.uploadButton.interactable = true;
        this.downloadButton.interactable = true;
        this.List();
    }

    private void BuildResults(UnityGoogleDrive.Data.FileList fileList)
    {
        results = new Dictionary<string, string>();
        Debug.Log("Results: ");
        foreach (var dir in fileList.Files)
        {
            var fileInfo = string.Format("Name: {0} Size: {1:0.00}MB Created: {2:dd.MM.yyyy}",
                dir.Name,
                dir.Size * .000001f,
                dir.CreatedTime);
            results.Add(dir.Id, fileInfo);

            Debug.Log(fileInfo);
        }
    }
}
