using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWorld : MonoBehaviour
{
    public GameObject world;
    public GameObject loadedWorld;
    private string saveFile = "wish2/json";
    // Start is called before the first frame update
    void Start()
    {
            WorldGenerator.Instance.GenerateTerrain();
        /*if (!File.Exists(Application.dataPath + saveFile))
        {
            WorldGenerator.Instance.GenerateTerrain();
        }
        else
        {
            Load();
        }*/

    }
    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            Save();
            Debug.Log("Saved world");
        }
    }

    // Update is called once per frame
    void Load()
    {
        JSON myObject = new JSON();
        string json = File.ReadAllText(Application.dataPath + saveFile);
        JSON loaded = JsonUtility.FromJson<JSON>(json);
        Instantiate(loaded.loadedWorld);

        //score = loaded.level;
        Debug.Log("Loaded");

    }
    void Save()
    {
        JSON myObject = new JSON();
        // myObject.level = score;       
        //myObject.newBlock = newBlocks;
        myObject.loadedWorld = loadedWorld;

        string json = JsonUtility.ToJson(myObject);

        File.WriteAllText(Application.dataPath + saveFile, json);
        Debug.Log(json + "saved");
    }
    private void OnApplicationQuit()
    {
        //Save();
    }

    [Serializable]
    public class JSON
    {
        public GameObject loadedWorld;
    }
}
