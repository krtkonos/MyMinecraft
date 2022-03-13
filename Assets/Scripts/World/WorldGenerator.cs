using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WorldGenerator : MonoBehaviour
{    
    public GameObject player;
    public Transform playerPos;
    public GameObject collid;         
    public GameObject parent;         
    public int sizeX; // horizontal length
    public int sizeZ; // horizontal length 2
    public int maxY; // height      

    public int groundHeight;
    public float terDetail;
    public float terHeight;
    int seed;
    int bdHP;

    public Vector3 plyPos;
    
    private GameObject[] blocks = new GameObject[4];
    public static WorldGenerator Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GetReferences();
        seed = 100076;
        Debug.Log(seed);
    }


    private void Start()
    {
        //GenerateTerrain();
    }
    private void Update()
    {        
        //bdHP = 1000;        
    }
    


    public void GenerateTerrain() // generate terrain
    {
        Debug.Log("Terraingenerated");
 
        for (int x = 0; x < sizeX; x++) 
        {
            for(int z = 0; z < sizeZ; z++)
            {
                maxY = (int)(Mathf.PerlinNoise((x / 2 + seed) / terDetail, (z / 2 + seed) / terDetail) * terHeight);
                maxY += groundHeight;
                GameObject grass = Instantiate(blocks[0], new Vector3(x, maxY, z), Quaternion.identity);
                grass.transform.SetParent(parent.transform);
                grass.GetComponent<BlockScript>().maxHealth = 10;

                for (int y = 0; y < maxY; y++)
                {
                    int dirtLayers = Random.Range(1, 5);
                    if(y >= maxY - dirtLayers)
                    {
                        GameObject dirt = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity);
                        dirt.transform.SetParent(parent.transform);
                        dirt.GetComponent<BlockScript>().maxHealth = 20;
                    }
                    else if (y < 1)
                    {
                        GameObject bedrock = Instantiate(blocks[3], new Vector3(x, y, z), Quaternion.identity);
                        bedrock.transform.SetParent(parent.transform);
                        bedrock.GetComponent<BlockScript>().maxHealth = 10000;
                    }
                    else
                    {
                        GameObject stone = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity);
                        stone.transform.SetParent(parent.transform);
                        stone.GetComponent<BlockScript>().maxHealth = 30; 
                    }
                }
                    

                if (x == (sizeX / 2) && z == (sizeZ / 2))
                {
                    GameObject coll = Instantiate(collid, new Vector3(x, maxY, z), Quaternion.identity);
                    coll.transform.SetParent(parent.transform);               
                }
                if (x == (int)(sizeX / 2) && z == (int)(sizeZ / 2))
                {
                    Instantiate(player, new Vector3(x, maxY + 3, z), Quaternion.identity);
                }
            }
        }
    }
    public void GenerateNewTerrain(int posX, int posZ)
    {
        Debug.Log("new terrain generated");

        for (int x = posX-16; x < posX-16 + sizeX; x++)
        {
            for(int z = posZ-16; z < posZ-16 + sizeZ; z++)
            {
                maxY= (int)(Mathf.PerlinNoise((x / 2 + seed) / terDetail, (z / 2 + seed) / terDetail) * terHeight);
                maxY += groundHeight;
                GameObject grass = Instantiate(blocks[0], new Vector3(x, maxY, z), Quaternion.identity)as GameObject;
                grass.transform.SetParent(parent.transform);
                grass.GetComponent<BlockScript>().maxHealth = 10;

                for (int y = 0; y < maxY; y++)
                {
                    int dirtLayers = Random.Range(1, 5);
                    if(y >= maxY - dirtLayers)
                    {
                        GameObject dirt = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        dirt.transform.SetParent(parent.transform);
                        dirt.GetComponent<BlockScript>().maxHealth = 20;                        
                    }
                    else if (y < 1)
                    {
                        GameObject bedrock = Instantiate(blocks[3], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        bedrock.transform.SetParent(parent.transform);
                        bedrock.GetComponent<BlockScript>().maxHealth = 10000;                        
                    }
                    else
                    {
                        GameObject stone = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        stone.transform.SetParent(parent.transform);
                        stone.GetComponent<BlockScript>().maxHealth = 30;                        
                    }
                }                
            }            
        }        
    }

    void GetReferences()
    {
        parent = GameObject.FindWithTag("Environment");
        blocks[0] = Resources.Load<GameObject>("Blocks/grassBlock");
        blocks[1] = Resources.Load<GameObject>("Blocks/stoneBlock");
        blocks[2] = Resources.Load<GameObject>("Blocks/dirtBlock");
        blocks[3] = Resources.Load<GameObject>("Blocks/bedrockBlock");
    }
}