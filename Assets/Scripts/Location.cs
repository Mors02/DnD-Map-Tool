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
    // Start is called before the first frame update
    void Start()
    {
        canvas.worldCamera = Camera.main;
        this.Save(locationName, imageIndex);
    }

    public void Reveal()
    {
        anim.SetTrigger("Reveal");
        Debug.Log("CLick su " + this.gameObject.name);
    }

    public void Hide()
    {
        anim.SetTrigger("Hide");
    }

    public void Save(string name, int imageIndex)
    {
        this.locationName = name;
        this.text.text = locationName;
        this.image.sprite = GameAssets.i.mapSymbols[imageIndex];
        this.imageIndex = imageIndex;
        //get the correct symbol sprite from the gameassets based on the imageindex returned
    }
}
