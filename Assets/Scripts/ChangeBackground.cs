using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.IO;
using UnityEngine.Networking;

public class ChangeBackground : MonoBehaviour
{
    string path;
    public Texture2D tex;
    [SerializeField]
    SpriteRenderer sRenderer;
    
    //Image background;
    private void Awake()
    {
        //image = GetComponentInChildren<RawImage>();
        sRenderer = GetComponent<SpriteRenderer>();
    }

    public void OpenExplorer()
    {
        path = EditorUtility.OpenFilePanel("Overwrite with png", "", "png");
        GetImage();
    }

    void GetImage()
    {
        if (path != null)
        {
            UpdateImage();
        }
    }

    void UpdateImage()
    {
        StartCoroutine(DownloadImage());
    }

    IEnumerator DownloadImage()
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture("file:///" + path);
        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
        {
            //image.texture = .texture;
            tex = (((DownloadHandlerTexture)request.downloadHandler).texture as Texture2D);
            LoadImage(tex);

           // Debug.Log( *100 + " " + sRenderer.bounds.size.y*100);
            //background.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }
            
    }

    public void LoadImage(Texture2D tex)
    {
        float width = tex.width, height = tex.height;
        
        float desiredPpu = (width / 3840f) * 100; //3840 is the desired size
        sRenderer.sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), desiredPpu);
    }
}
