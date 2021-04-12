using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    [SerializeField] BlockScript grassPrefab;
    [SerializeField] BlockScript dirtPrefab;
    [SerializeField] BlockScript stonePrefab;
    [SerializeField] BlockScript bedrockPrefab;
    [SerializeField] MyController playerPrefab;
    [SerializeField] WorldColliderSCript colliderPrefab;
    [SerializeField] WorldGenerator wg;
    public static List<BlockScript> blocks = new List<BlockScript>();
    public static List<MyController> player = new List<MyController>();
    public static List<WorldColliderSCript> coll = new List<WorldColliderSCript>();
    public string BLOCK_SUB = "/file1.txt";
    public string BLOCK_COUNT_SUB = "/file2.txt";
    public string PLAYER_SUB = "/file3.txt";
    public string PLAYER_COUNT_SUB = "/file4.txt";
    public string COLLIDER_SUB = "/file5.txt";
    public string COLLIDER_COUNT_SUB = "/file6.txt";

    private void Awake()
    {
        LoadBlock();
        LoadCollider();
        LoadPlayer();
    }

    private void OnApplicationQuit()
    {
        SaveBlock();
        SaveCollider();
        SavePlayer();
    }
    
    public void SaveBlock()
    {
        Debug.Log("Save savesystem:");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + BLOCK_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + BLOCK_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, blocks.Count);
        countStream.Close();

        for (int i = 0; i < blocks.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            BlockData data = new BlockData(blocks[i]);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }
    void LoadBlock() 
    {
        Debug.Log("load save szstem");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + BLOCK_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + BLOCK_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int blockCount = 0;

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);

            blockCount = (int)formatter.Deserialize(countStream);
        }
        else
        {
            wg.GenerateTerrain();
            Debug.LogError("Path Not found in " + countPath);
        }
        for (int i = 0; i < blockCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                BlockData data = formatter.Deserialize(stream) as BlockData;

                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);

               // BlockScript grass = Instantiate(grassPrefab, position, Quaternion.identity);
                //grass.transform.SetParent(GameObject.FindWithTag("Enviro").transform);
                
                BlockScript dirt = Instantiate(dirtPrefab, position, Quaternion.identity);
                dirt.transform.SetParent(GameObject.FindWithTag("Enviro").transform);
               /* BlockScript stone = Instantiate(stonePrefab, position, Quaternion.identity);
                stone.transform.SetParent(GameObject.FindWithTag("Enviro").transform);
                BlockScript bedrock = Instantiate(bedrockPrefab, position, Quaternion.identity);
                bedrock.transform.SetParent(GameObject.FindWithTag("Enviro").transform); */               


                //grass.currentHealth = data.currentHealth;
                //grass.id = data.id;
                dirt.currentHealth = data.currentHealth;
                dirt.id = data.id;
                /*stone.currentHealth = data.currentHealth;
                stone.id = data.id;
                bedrock.currentHealth = data.currentHealth;
                bedrock.id = data.id;*/
                

            }
            else
            {
                Debug.LogError("Path Not found in " + path + i);
            }
        }
    }
    void SavePlayer()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + PLAYER_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + PLAYER_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, player.Count);
        countStream.Close();

        for (int i = 0; i < player.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            BlockData data = new BlockData(player[0]);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        Debug.Log("Save save Playersystem:");
    }
    void LoadPlayer()
    {
        Debug.Log("load savePlayer szstem");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + PLAYER_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + PLAYER_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int playerCount = 0;

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);

            playerCount = (int)formatter.Deserialize(countStream);
        }
        else
        {
            Debug.LogError("Path Not found in " + countPath);
        }
        for (int i = 0; i < playerCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                BlockData data = formatter.Deserialize(stream) as BlockData;

                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);

                MyController player = Instantiate(playerPrefab, position, Quaternion.identity);
                //grass.transform.SetParent(GameObject.FindWithTag("Enviro").transform);               
                

            }
            else
            {
                Debug.LogError("Path Not found in " + path + i);
            }
        }
    }
    void SaveCollider()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + COLLIDER_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + PLAYER_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, coll.Count);
        countStream.Close();

        for (int i = 0; i < coll.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            BlockData data = new BlockData(coll[0]);

            formatter.Serialize(stream, data);
            stream.Close();
        }
        Debug.Log("Save save Playersystem:");
    }
    void LoadCollider()
    {
        Debug.Log("load savePlayer szstem");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + COLLIDER_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + PLAYER_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int colliderCount = 0;

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);

            colliderCount = (int)formatter.Deserialize(countStream);
        }
        else
        {
            Debug.LogError("Path Not found in " + countPath);
        }
        for (int i = 0; i < colliderCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                BlockData data = formatter.Deserialize(stream) as BlockData;

                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);

                WorldColliderSCript collide = Instantiate(colliderPrefab, position, Quaternion.identity);
                collide.transform.SetParent(GameObject.FindWithTag("Enviro").transform);
                //collide.transform.SetParent(GameObject.FindWithTag("Enviro").transform);
                //grass.transform.SetParent(GameObject.FindWithTag("Enviro").transform);               


            }
            else
            {
                Debug.LogError("Path Not found in " + path + i);
            }
        }
    }
}
