using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    TMP_InputField nameText, dimText;

    bool visible;


    // Start is called before the first frame update
    void Start()
    {
        this.rect = gameObject.GetComponent<RectTransform>();
        cm = Camera.main.GetComponent<CameraMovement>();
        sl = Camera.main.GetComponent<KeyboardCommands>();
        symbols = GameObject.FindGameObjectWithTag("Symbols").GetComponent<Symbols>();
        this.OnClose();

    }

    private void Update()
    {
        if (this.visible)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                this.Save();
            }

        }
    }

    public void Close()
    {
        this.OnClose();
    }

    public void Save()
    {
        this.currentLocation.Save(nameText.text, symbols.lastSelected, dimText.text);
        this.OnClose();
    }

    private void OnClose()
    {
        cm.dragging = false;
        sl.dragging = false;
        this.visible = false;
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
        dimText.text = location.transform.localScale.x.ToString("F1");
        this.visible = true;
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
