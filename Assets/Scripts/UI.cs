using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    Vector2 openPosition = new Vector2(3, -20);

    [SerializeField]
    Vector2 closePosition = new Vector2(3, 400);

    private RectTransform rect;

    CameraMovement cm;
    KeyboardCommands sl;

    Symbols symbols;

    Location currentLocation;

    [SerializeField]
    TMP_InputField nameText;

    // Start is called before the first frame update
    void Start()
    {
        this.rect = gameObject.GetComponent<RectTransform>();
        cm = Camera.main.GetComponent<CameraMovement>();
        sl = Camera.main.GetComponent<KeyboardCommands>();
        symbols = GameObject.FindGameObjectWithTag("Symbols").GetComponent<Symbols>();
        this.OnClose();       
    }
    

    public void Close()
    {
        this.OnClose();
    }

    public void Save()
    {
        this.currentLocation.Save(nameText.text, symbols.lastSelected);
        this.OnClose();
    }

    private void OnClose()
    {
        cm.dragging = false;
        sl.dragging = false;
        this.rect.anchoredPosition = closePosition;
    }

    public void Open(Location location)
    {
        cm.dragging = true;
        sl.dragging = true;
        this.rect.anchoredPosition = openPosition;
        this.currentLocation = location;
        nameText.text = location.locationName;
        symbols.SetSelected(location.imageIndex);
    }

    public void Hide()
    {
        this.OnClose();
        currentLocation.Hide();
    }

    public void Destroy()
    {
        this.OnClose();
        Destroy(currentLocation.gameObject);        
    }
}
