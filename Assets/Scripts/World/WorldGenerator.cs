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
    public int sizeX; // horizontal length
    public int sizeZ; // horizontal length 2
    public int maxY; // height      

    public int groundHeight;
    public float terDetail;
    public float terHeight;
    int seed;
    int bdHP;

    public Vector3 plyPos;
    
    public GameObject[] blocks;

    

    private void Start()
    {
            seed = 100076;
            Debug.Log(seed);       
    }
    private void Update()
    {        
        bdHP = 1000;        
    }
    


    public void GenerateTerrain() // generate terrain
    {
        Debug.Log("Terraingenerated");
 
        for (int x = 0; x < sizeX; x++) 
        {
            for(int z = 0; z < sizeZ; z++)
            {
                maxY= (int)(Mathf.PerlinNoise((x / 2 + seed) / terDetail, (z / 2 + seed) / terDetail) * terHeight);
                maxY += groundHeight;
                GameObject grass = Instantiate(blocks[0], new Vector3(x, maxY, z), Quaternion.identity)as GameObject;
                grass.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                var grassHP = grass.GetComponent<BlockScript>();
                grassHP.maxHealth = 10;

                for (int y = 0; y < maxY; y++)
                {
                    int dirtLayers = Random.Range(1, 5);
                    if(y >= maxY - dirtLayers)
                    {
                        GameObject dirt = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        dirt.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var dirtHP = dirt.GetComponent<BlockScript>();
                        dirtHP.maxHealth = 20;
                    }
                    else if (y < 1)
                    {
                        GameObject bedrock = Instantiate(blocks[3], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        bedrock.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var bedrockHP = bedrock.GetComponent<BlockScript>();
                        bedrockHP.maxHealth = bdHP;
                    }
                    else
                    {
                        GameObject stone = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        stone.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var stoneHP = stone.GetComponent<BlockScript>();
                        stoneHP.maxHealth = 30;                        
                    }
                }
                    

                if (x == (int)(sizeX / 2) && z == (int)(sizeZ / 2))
                {
                    GameObject coll = Instantiate(collid, new Vector3(x, maxY, z), Quaternion.identity)as GameObject;
                    coll.transform.SetParent(GameObject.FindWithTag("Environment").transform);               
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
                grass.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                var grassHP = grass.GetComponent<BlockScript>();
                grassHP.maxHealth = 10;

                for (int y = 0; y < maxY; y++)
                {
                    int dirtLayers = Random.Range(1, 5);
                    if(y >= maxY - dirtLayers)
                    {
                        GameObject dirt = Instantiate(blocks[2], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        dirt.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var dirtHP = dirt.GetComponent<BlockScript>();
                        dirtHP.maxHealth = 20;                        
                    }
                    else if (y < 1)
                    {
                        GameObject bedrock = Instantiate(blocks[3], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        bedrock.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var bedrockHP = bedrock.GetComponent<BlockScript>();
                        bedrockHP.maxHealth = bdHP;                        
                    }
                    else
                    {
                        GameObject stone = Instantiate(blocks[1], new Vector3(x, y, z), Quaternion.identity) as GameObject;
                        stone.transform.SetParent(GameObject.FindWithTag("Environment").transform);
                        var stoneHP = stone.GetComponent<BlockScript>();
                        stoneHP.maxHealth = 30;                        
                    }
                }                
            }            
        }        
    }  
}