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
    public RawImage image;
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
            Texture2D tex = (((DownloadHandlerTexture)request.downloadHandler).texture as Texture2D);
            float width = tex.width, height = tex.height;
            float desiredPpu = (38.4f/width) * 100; //3840 / 100
            Debug.Log(desiredPpu);
            sRenderer.sprite = Sprite.Create(tex, new Rect(-0, -0, width, height), new Vector2(0.5f, 0.5f), desiredPpu);

           // Debug.Log( *100 + " " + sRenderer.bounds.size.y*100);
            //background.sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }
            
    }
}
