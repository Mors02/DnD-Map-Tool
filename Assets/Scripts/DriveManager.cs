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
        //a check should be made whether the folder already exists
        if (ss.dirName != "")
        {
            //create the folder
            UploadFilePath = ss.basePath;
            var file = new UnityGoogleDrive.Data.File { Name = Path.GetFileName(UploadFilePath), MimeType = "application/vnd.google-apps.folder"};
            file.Parents = new List<string> { "appDataFolder" };
            createRequest = GoogleDriveFiles.Create(file);
            createRequest.Fields = new List<string> { "id", "name", "size", "createdTime" };
            

            //UploadFilePath = ss.backgroundPath;
            //var content = File.ReadAllBytes(UploadFilePath);

            
            
            
            this.uploadButton.interactable = false;
            this.downloadButton.interactable = false;
            createRequest.Send().OnDone += UploadFiles;
        }
    }

    public void List(string folderId = null)
    {
        listRequest = GoogleDriveFiles.List();
        listRequest.Fields = new List<string> { "files(id, name, size)" };
        listRequest.Spaces = "appDataFolder";
        //listRequest.Q = $"'{folderId}' in parents";
        //Debug.Log(listRequest.Q);
        listRequest.PageSize = 10;
        listRequest.Send().OnDone += ListFiles;
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
        
    }

    private void UploadFiles(UnityGoogleDrive.Data.File dir)
    {
        this.uploadButton.interactable = true;
        this.downloadButton.interactable = true;
        if (Directory.Exists(ss.basePath))
        {
            DirectoryInfo d = new DirectoryInfo(ss.basePath);
            foreach (var file in d.GetFiles())
            {
                //Debug.Log(file.Name);
                UploadFilePath = file.FullName;
                var content = File.ReadAllBytes(UploadFilePath);
                var fileReq = new UnityGoogleDrive.Data.File { Name = Path.GetFileName(UploadFilePath), Content = content, Parents = new List<string> { "appDataFolder, " + dir.Id } };
                
                fileReq.Parents = new List<string> { dir.Id };
                GoogleDriveFiles.CreateRequest createRequest = GoogleDriveFiles.Create(fileReq);
                //FilesResource.CreateMediaUpload request;
                //GoogleDriveFiles.CreateRequest request;
                /*using (var stream = new FileStream(file.FullName,
                           FileMode.Open))
                {
                    // Create a new file, with metadata and stream.
                    request = GoogleDriveFiles.Create(
                        fileReq, stream, "image/jpeg");
                    request.Fields = new List<string> { "id", "name", "size", "createdTime" };
                    request.Send().OnDone += PrintResult;
                }*/
                createRequest.Fields = new List<string> { "id", "name", "size", "createdTime" };
                createRequest.Send().OnDone += PrintResult;
            }
        }
    }

    private void ListFiles(UnityGoogleDrive.Data.FileList fileList)
    {
        foreach (var dir in fileList.Files)
        {
            var fileInfo = string.Format("Name: {0} Size: {1:0.00}MB Parents: {2} \nID: {3}",
                dir.Name,
                dir.Size * .000001f,
                dir.Parents,
                dir.Id);
            Debug.Log(fileInfo);
            List(dir.Id);
        }
    }

    private void BuildResults(UnityGoogleDrive.Data.FileList fileList)
    {
        results = new Dictionary<string, string>();
        Debug.Log("Res: ");
        foreach (var dir in fileList.Files)
        {
            var fileInfo = string.Format("Name: {0} Size: {1:0.00}MB Parents: {2} \nID: {3}",
                dir.Name,
                dir.Size * .000001f,
                dir.Parents,
                dir.Id);
            results.Add(dir.Id, fileInfo);

            Debug.Log(fileInfo);
        }
    }
}
