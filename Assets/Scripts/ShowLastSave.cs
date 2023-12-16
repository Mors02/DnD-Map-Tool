using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowLastSave : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void UpdateCounter()
    {
        int seconds = ((int)SavingSystem.lastSave % 60);
        int minutes = ((int)SavingSystem.lastSave / 60);
        this.text.text = "Ultimo salvataggio: " + string.Format("{0:00}:{1:00}", minutes, seconds) + " fa.";
    }

}
