using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    [SerializeField]
    Image arrow;

    bool isVisible;

    KeyboardCommands kb;

    private void Start()
    {
        this.kb = Camera.main.GetComponent<KeyboardCommands>();
        isVisible = false;
    }

    // Start is called before the first frame update
    public void ChangeState()
    {
        if (isVisible)
        {
            this.menu.SetActive(false);
        } else
        {
            this.menu.SetActive(true);
        }

        this.arrow.transform.Rotate(0, 0, 180);
        this.isVisible = !isVisible;
    }

    public void Save()
    {
        kb.Save();
    }

    public void Load()
    {
        kb.Load();
    }

    public void Screen()
    {
        kb.Screen();
    }

    public void ChangeBackground()
    {
        kb.ChangeBackground();
    }

    public void Spawn()
    {
        kb.Spawn(new Vector2(0, 0));
    }

}
