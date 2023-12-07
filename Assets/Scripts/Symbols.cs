using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Symbols : MonoBehaviour
{
    [SerializeField]
    GameObject parent;

    SymbolScript[] symbols;

    public int symbolNum = 0, lastSelected = 0;

    // Start is called before the first frame update
    void Start()
    {
        symbols = new SymbolScript[GameAssets.i.mapSymbols.Length];

        for (int i = 0; i < symbols.Length; i++)
        {
            GameObject button = Instantiate(GameAssets.i.buttonPrefab, parent.transform);
            Sprite sprite = GameAssets.i.mapSymbols[i];
            button.GetComponent<Image>().sprite = sprite;
            button.name = sprite.name;
            int buttonIndex = i;
            button.GetComponent<Button>().onClick.AddListener(delegate { SetSelected(buttonIndex); } );
            symbols[symbolNum++] = button.GetComponent<SymbolScript>() ;
        }
        //PrintSymbols();
    }

    public void SetSelected(int index)
    {
        //Debug.Log("i: " + index + " ls: " + lastSelected);
        symbols[lastSelected].Unselect();
        symbols[index].Select();
        lastSelected = index;
        
    }

    private void PrintSymbols()
    {
        for(int i = 0; i < symbols.Length; i++)
        {
            Debug.Log(symbols[i].gameObject.name + " - " + i);
        }
    }
}
