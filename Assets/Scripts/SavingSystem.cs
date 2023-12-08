using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.IO;

public class SavingSystem : MonoBehaviour
{
    public LocationData[] locations;

    public string locationPath, partyPath;
    // Start is called before the first frame update
    void Start()
    {
        locationPath = Application.persistentDataPath + "/LocationData.dat";
        partyPath = Application.persistentDataPath + "/PartyData.dat";
        Load();
    }

    public bool Save()
    {
        //count the number of locations present
        locations = new LocationData[transform.childCount];

        //set each location in the correct format
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Location location = child.gameObject.GetComponent<Location>();
            
            //cannot use a custom constructor because it would break serialization
            locations[i] = new LocationData();
            locations[i].x = child.position.x;
            locations[i].y = child.position.y;
            locations[i].lName = location.locationName;
            locations[i].iIndex = location.imageIndex;
            locations[i].hidden = location.hidden;
        }

        //Serialize the array
        string json = JsonHelper.ToJson(locations);
        //encode
        byte[] bytesToEncode = Encoding.UTF8.GetBytes(json);
        string encodedText = Convert.ToBase64String(bytesToEncode);
        //write on file
        File.WriteAllText(locationPath, encodedText);

        //Get the party
        GameObject party = GameObject.FindGameObjectWithTag("Party");
        //save the party location in a serializable format
        LocationData partyData = new LocationData();
        partyData.x = party.transform.position.x;
        partyData.y = party.transform.position.y;
        //same as before
        json = JsonUtility.ToJson(partyData);
        bytesToEncode = Encoding.UTF8.GetBytes(json);
        encodedText = Convert.ToBase64String(bytesToEncode);
        File.WriteAllText(partyPath, encodedText);

        return (File.Exists(locationPath) && File.Exists(partyPath));
    }
    

    public bool Load()
    {
        if (File.Exists(locationPath))
        {
            foreach(Transform child in transform)
            {
                Destroy(child.gameObject);
            }

            string encodedText = File.ReadAllText(locationPath);
            byte[] decodedBytes = Convert.FromBase64String(encodedText);
            string json = Encoding.UTF8.GetString(decodedBytes);

            locations = JsonHelper.FromJson<LocationData>(json);

            foreach (LocationData location in locations)
            {
                Vector2 pos = new Vector2(location.x, location.y);
                GameObject loc = Instantiate(GameAssets.i.locationPrefab, pos, Quaternion.identity);
                loc.transform.SetParent(this.gameObject.transform);
                loc.GetComponent<Location>().Load(location.lName, location.iIndex, location.hidden);
            }
        }
        else 
            return false;

        if (File.Exists(partyPath))
        {
            string encodedText = File.ReadAllText(partyPath);
            byte[] decodedBytes = Convert.FromBase64String(encodedText);
            string json = Encoding.UTF8.GetString(decodedBytes);

            LocationData partyData = JsonUtility.FromJson<LocationData>(json);
            Vector2 pos = new Vector2(partyData.x, partyData.y);
            GameObject party = GameObject.FindGameObjectWithTag("Party");
            party.gameObject.transform.position = pos;


        }
        else 
            return false;

        return true;

    }
}

[Serializable]
public class LocationData
{
    public float x;
    public float y;
    public string lName;
    public int iIndex;
    public bool hidden;
    /**
     * 
     * public LocationData(float x, float y, string lName, int iIndex)
    {
        this.x = x;
        this.y = y;
        this.lName = lName;
        this.iIndex = iIndex;
    }
     */
}
