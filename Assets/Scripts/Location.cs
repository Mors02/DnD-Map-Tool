using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Location : MonoBehaviour
{

    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    private Animator anim;

    public string locationName;

    [SerializeField]
    private Image image;

    [SerializeField]
    private TMP_Text text;

    public int imageIndex = 0;

    public bool hidden = true;
    // Start is called before the first frame update
    void Awake()
    {
        canvas.worldCamera = Camera.main;
        this.Save(locationName, imageIndex, "1");
    }

    public void Reveal()
    {
        anim.SetTrigger("Reveal");
        hidden = false;        
    }

    private void InstaReveal()
    {
        anim.SetTrigger("InstaReveal");
    }

    public void Hide()
    {
        anim.SetTrigger("Hide");
        hidden = true;
    }

    public void Save(string name, int imageIndex, string dim)
    {
        this.locationName = name;
        this.text.text = locationName;
        this.image.sprite = GameAssets.i.mapSymbols[imageIndex];
        //get the correct symbol sprite from the gameassets based on the imageindex returned
        this.imageIndex = imageIndex;
        
        this.transform.localScale = new Vector2(float.Parse(dim), float.Parse(dim));
    }

    public void Load(string name, int imageIndex, bool hidden, float dim)
    {
       // Debug.Log(width.ToString() + ", " + height.ToString());
        Save(name, imageIndex, dim.ToString());
        if (!hidden)
        {
            this.Reveal();
        }
    }
}
