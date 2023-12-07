using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SymbolScript : MonoBehaviour
{
    [SerializeField]
    Image image;

    public void Select()
    {
        this.image.color = new Color(0, 0, 0, 1);
    }

    public void Unselect()
    {
        this.image.color = new Color(0, 0, 0, 0);
    }
}
