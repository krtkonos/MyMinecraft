using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class WorldGenerator : MonoBehaviour
{    
    public GameObject player;
    public Transform playerPos;
    public GameObject collid;         
    public int sizeX;
    public int sizeZ;
    public int maxY;

    public int firstX = 32;
    public int nextX = 0;
    public int firstZ = 32;
    public int nextZ = 0;    
    public static string saveFile = "/johnsonworld4.json";    

    public int groundHeight;
    public float terDetail;
    public float terHeight;
    int seed;
    int bdHP;

    public Vector3 plyPos;
    
    public GameObject[] blocks;

    

    private void Start()
    {
        JSON myObject = new JSON();
       /* if (File.Exists(Application.dataPath + saveFile))
        {            
            Load();
        }
        else
        {*/
            //seed = Random.Range(100000, 999999);
            seed = 100076;
            Debug.Log(seed);
        //Instantiate(player, new Vector3(15, 50, 15), Quaternion.identity);
        //GenerateTerrain();      
        //}
    }
    private void Update()
    {        
        bdHP = 1000;
        
    }
    


    public void GenerateTerrain()
    {
        Debug.Log("Terraingenerated");
        //JSON save = new JSON();
        for (float x = 0; x < sizeX; x++)
        {
            for(float z = 0; z < sizeZ; z++)
            {
                maxY= (int)(Mathf.PerlinNoise((x / 2 + seed) / terDetail, (z / 2 + seed) / terDetail) * terHeight);
                maxY += groundHeight;
                GameObject grass = Instantiate(blocks[0], new Vector3(x, maxY, z), Quaternion.identity)as GameObject;
                grass.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                var grassHP = grass.GetComponent<BlockScript>();
                grassHP.maxHealth = 10;
                //blockObj.Add(blocks[0]);
                //save.blockVector.Add(grass.transform.position);

                for (int y = 0; y < maxY; y++)
                {
                    int dirtLayers = Random.Range(1, 5);
                    if(y >= maxY - dirtLayers)
                    {
                        GameObject dirt = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        dirt.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var dirtHP = dirt.GetComponent<BlockScript>();
                        dirtHP.maxHealth = 20;
                        //dirtObj.Add(blocks[2]);
                        //save.blockVector.Add(dirt.transform.position);
                        
                    }
                    else if (y < 1)
                    {
                        GameObject bedrock = Instantiate(blocks[3], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        bedrock.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var bedrockHP = bedrock.GetComponent<BlockScript>();
                        bedrockHP.maxHealth = bdHP;
                        //bedrockObj.Add(blocks[3]);
                        //save.blockVector.Add(bedrock.transform.position);
                    }
                    else
                    {
                        GameObject stone = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        stone.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var stoneHP = stone.GetComponent<BlockScript>();
                        stoneHP.maxHealth = 30;
                        //stoneObj.Add(blocks[1]);
                        //save.blockVector.Add(stone.transform.position);
                    }
                }
                    

                if (x == (int)(sizeX / 2) && z == (int)(sizeZ / 2))
                {
                    GameObject coll = Instantiate(collid, new Vector3(x, maxY, z), Quaternion.identity)as GameObject;
                    coll.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                    //collObj.Add(collid);
                    //save.blockVector.Add(collid.transform.position);
                }
                /*if (x == (int)(sizeX / 2) && z == (int)(sizeZ / 2))
                {
                    Instantiate(player, new Vector3(x, maxY + 3, z), Quaternion.identity);
                }*/
                /*if (!File.Exists(Application.dataPath + "/wish.json"))
                {*/
                    if (x == (int)(sizeX / 2) && z == (int)(sizeZ / 2))
                    {
                        Instantiate(player, new Vector3(x, maxY + 3, z), Quaternion.identity);
                    }
                //}    
            }
        }
        /*if (File.Exists(Application.dataPath + "/wish.json"))
        {
            Load();
        }*/
        //save.seed = seed;


    }
    public void GenerateNewTerrain(int posX, int posZ)
    {
        Debug.Log("new terrain generated");
        /*GameObject coll = Instantiate(collid, new Vector3(posX - 16, maxY - 10,
                    posZ - 16), Quaternion.identity);
        coll.transform.SetParent(GameObject.FindWithTag("Enviro").transform);*/
        for (int x = posX-16; x < posX-16 + sizeX; x++)
        {
            for(int z = posZ-16; z < posZ-16 + sizeZ; z++)
            {
                maxY= (int)(Mathf.PerlinNoise((x / 2 + seed) / terDetail, (z / 2 + seed) / terDetail) * terHeight);
                maxY += groundHeight;
                GameObject grass = Instantiate(blocks[0], new Vector3(x, maxY, z), Quaternion.identity)as GameObject;
                grass.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                //grass.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                var grassHP = grass.GetComponent<BlockScript>();
                grassHP.maxHealth = 10;
                //blockObj.Add(blocks[0]);

                for (int y = 0; y < maxY; y++)
                {
                    int dirtLayers = Random.Range(1, 5);
                    if(y >= maxY - dirtLayers)
                    {
                        GameObject dirt = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        dirt.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var dirtHP = dirt.GetComponent<BlockScript>();
                        dirtHP.maxHealth = 20;
                        //dirtObj.Add(blocks[2]);
                        //save.blockVector.Add(blocks[2].transform.position);
                        
                    }
                    else if (y < 1)
                    {
                        GameObject bedrock = Instantiate(blocks[3], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        bedrock.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var bedrockHP = bedrock.GetComponent<BlockScript>();
                        bedrockHP.maxHealth = bdHP;
                        //bedrockObj.Add(blocks[3]);
                        //save.blockVector.Add(blocks[3].transform.position);
                    }
                    else
                    {
                        GameObject stone = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        stone.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var stoneHP = stone.GetComponent<BlockScript>();
                        stoneHP.maxHealth = 30;
                        //stoneObj.Add(stone);
                        //save.blockVector.Add(stone.transform.position);
                    }

                }
                /*if (x == (int)(sizeX / 2) && z == (int)(sizeZ / 2))
                {
                    GameObject coll = Instantiate(collid, new Vector3(transform.position.x, transform.position.y - 10,
                    transform.position.z), Quaternion.identity);
                    coll.transform.SetParent(GameObject.FindWithTag("Enviro").transform);
                }*/
            }
            
        }
        
    }
    void Blocks()
    {
        GameObject grass = Instantiate(blocks[0], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        GameObject stone = Instantiate(blocks[1], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        GameObject dirt = Instantiate(blocks[2], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        GameObject bedrock = Instantiate(blocks[3], new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

    }
    void Load()
    {
        JSON myObject = new JSON();
        string json = File.ReadAllText(Application.dataPath + saveFile);
        JSON loaded = JsonUtility.FromJson<JSON>(json);
        seed = loaded.jsonSeed;        
        plyPos = loaded.JSONpos;
        //Instantiate(player, plyPos, Quaternion.identity);
        Instantiate(player, plyPos, Quaternion.identity);


        //score = loaded.level;
        Debug.Log("Loaded");

    }
    void Save()
    {
        JSON myObject = new JSON();
        //myObject.JSONpos = MyController.pos;
        myObject.jsonSeed = seed;
        //myObject.loadedWorld = world;
        Debug.Log("Json seed: " + myObject.jsonSeed);

        string json = JsonUtility.ToJson(myObject);

        File.WriteAllText(Application.dataPath + saveFile, json);
        Debug.Log(json + "saved");

    }
    

    [System.Serializable]
    public class JSON
    {
        public int jsonSeed;
        public Vector3 JSONpos;        
    }

}