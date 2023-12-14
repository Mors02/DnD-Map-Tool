using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Project : MonoBehaviour
{

    public string path, dirName;
    public TMP_Text tPath, tDirName;
    public Image background;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetUp(string path, string dirName)
    {
        this.path = path;
        this.dirName = dirName;
        tPath.text = this.path;
        tDirName.text = this.dirName;
    }

    public void OnClick()
    {
        Debug.Log("Opening project " + dirName);
    }

    public void IsSelected(bool isSel)
    {
        if (isSel)
        {
            this.background.color = new Color32(200, 200, 200, 255);
        } else
        {
            this.background.color = new Color32(255, 255, 255, 255);
        }
    }
}
