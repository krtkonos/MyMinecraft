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
    [SerializeField] MyController mc;
    public static List<BlockScript> blocks = new List<BlockScript>();
    public static List<BlockScript> stoneB = new List<BlockScript>();
    public static List<BlockScript> dirtB = new List<BlockScript>();
    public static List<BlockScript> brB = new List<BlockScript>();
    public static MyController player = new MyController();
    
    public static List<WorldColliderSCript> coll = new List<WorldColliderSCript>();
    public string BLOCK_SUB = "/file1.txt";
    public string BLOCK_COUNT_SUB = "/file2.txt";
    public string DIRT_SUB = "/file1.txt";
    public string DIRT_COUNT_SUB = "/file2.txt";
    public string STONE_SUB = "/file1.txt";
    public string STONE_COUNT_SUB = "/file2.txt";
    public string BRB_SUB = "/file1.txt";
    public string BRB_COUNT_SUB = "/file2.txt";
    //public string PLAYER_COUNT_SUB = "/file4.txt";
    public string COLLIDER_SUB = "/file5.txt";
    public string COLLIDER_COUNT_SUB = "/file6.txt";
    public static string PLAYER_SUB = "/mine11.txt";

    private void Awake()
    {
        LoadBlock();
        LoadDirt();
        LoadStone();
        LoadBrb();
        LoadCollider();
        Load();        
    }

    /*private void OnApplicationQuit()
    {
        
    }*/
    private void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            SaveBlock();
            SaveCollider();
            SaveDirt();
            SaveStone();
            SaveBrb();            
            mc.Save();
        }
        if (Input.GetKeyDown("l"))
        {
            Instantiate(playerPrefab, new Vector3(0,50,0), Quaternion.identity);
        }
    }

    public void SaveBlock()//saveGrass
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
    public void SaveDirt()
    {
        Debug.Log("Save savesystem:");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + DIRT_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + DIRT_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, dirtB.Count);
        countStream.Close();
        
        for (int i = 0; i < dirtB.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            BlockData data = new BlockData(dirtB[i]);

            formatter.Serialize(stream, data);
            stream.Close();
        }   
    }
    public void SaveStone()
    {
        Debug.Log("Save savesystem:");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + STONE_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + STONE_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, stoneB.Count);
        countStream.Close();
        
        for (int i = 0; i < stoneB.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            BlockData data = new BlockData(stoneB[i]);

            formatter.Serialize(stream, data);
            stream.Close();
        }   
    }
    public void SaveBrb()
    {
        Debug.Log("Save savesystem:");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + BRB_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + BRB_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);

        formatter.Serialize(countStream, brB.Count);
        countStream.Close();
        
        for (int i = 0; i < brB.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            BlockData data = new BlockData(brB[i]);

            formatter.Serialize(stream, data);
            stream.Close();
        }   
    }
    void LoadBlock() //load grass
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

                BlockScript grass = Instantiate(grassPrefab, position, Quaternion.identity);
                grass.transform.SetParent(GameObject.FindWithTag("Enviro").transform);  
                grass.currentHealth = data.currentHealth;
                grass.id = data.id;
            }
            else
            {
                //Debug.LogError("Path Not found in " + path + i);
            }
        }   
    }
    void LoadDirt() 
    {
        Debug.Log("load save szstem");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + DIRT_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + DIRT_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int dirtBCount = 0;
        

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);            
            dirtBCount = (int)formatter.Deserialize(countStream);            
        }
        else
        {            
            //Debug.LogError("Path Not found in " + countPath);
        }
        
        for (int i = 0; i < dirtBCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                BlockData data = formatter.Deserialize(stream) as BlockData;

                stream.Close();

                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);
                
                BlockScript dirt = Instantiate(dirtPrefab, position, Quaternion.identity);
                dirt.transform.SetParent(GameObject.FindWithTag("Enviro").transform);                            

                dirt.currentHealth = data.currentHealth;
                dirt.id = data.id;                     
            }
            else
            {
                //Debug.LogError("Path Not found in " + path + i);
            }
        }   
    }
    void LoadStone() 
    {
        Debug.Log("load save szstem");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + STONE_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + STONE_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int stoneBCount = 0;
        

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);
            stoneBCount = (int)formatter.Deserialize(countStream);            
        }
        else
        {            
            //Debug.LogError("Path Not found in " + countPath);
        }

        for (int i = 0; i < stoneBCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                BlockData data = formatter.Deserialize(stream) as BlockData;
                stream.Close();
                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);

                BlockScript stone = Instantiate(stonePrefab, position, Quaternion.identity);
                stone.transform.SetParent(GameObject.FindWithTag("Enviro").transform);

                stone.currentHealth = data.currentHealth;
                stone.id = data.id;
            }
            else
            {
                //Debug.LogError("Path Not found in " + path + i);
            }
        }
    }
    void LoadBrb() 
    {
        Debug.Log("load save szstem");
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + BRB_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + BRB_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
        int beBCount = 0;
        

        if (File.Exists(countPath))
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open);
            beBCount = (int)formatter.Deserialize(countStream);            
        }
        else
        {
            
            //Debug.LogError("Path Not found in " + countPath);
        }

        for (int i = 0; i < beBCount; i++)
        {
            if (File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                BlockData data = formatter.Deserialize(stream) as BlockData;
                stream.Close();
                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]);

                BlockScript bedrock = Instantiate(bedrockPrefab, position, Quaternion.identity);
                bedrock.transform.SetParent(GameObject.FindWithTag("Enviro").transform);

                bedrock.currentHealth = data.currentHealth;
                bedrock.id = data.id;
            }
            else
            {
                //Debug.LogError("Path Not found in " + path + i);
            }
        }
    }
    
    void SaveCollider()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + COLLIDER_SUB + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + COLLIDER_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;

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
        string countPath = Application.persistentDataPath + COLLIDER_COUNT_SUB + SceneManager.GetActiveScene().buildIndex;
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

                Vector3 position = new Vector3(data.colliderPosition[0], data.colliderPosition[1], data.colliderPosition[2]);

                WorldColliderSCript collide = Instantiate(colliderPrefab, position, Quaternion.identity);
                collide.transform.SetParent(GameObject.FindWithTag("Enviro").transform); 
            }
            else
            {
                Debug.LogError("Path Not found in " + path + i);
            }
        }
    }
    public void PlayerSave()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + PLAYER_SUB;
        
        FileStream stream = new FileStream(path, FileMode.Create);

        BlockData data = new BlockData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public BlockData PlayerLoad()
    {
        string path = Application.persistentDataPath + PLAYER_SUB;
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            BlockData data = formatter.Deserialize(stream) as BlockData;
            stream.Close();
            Vector3 position = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);
            Instantiate(playerPrefab, position, Quaternion.identity);
            return data;
        }
        else
        {
            Debug.LogError("not found in " + path);
            return null;
        }
    }
    public void Load()
    {
        if (File.Exists(Application.dataPath + PLAYER_SUB))
        {
            JSON myObject = new JSON();
            string json = File.ReadAllText(Application.dataPath + PLAYER_SUB);
            JSON loaded = JsonUtility.FromJson<JSON>(json);
            Vector3 pos= loaded.JSONpos;
            Instantiate(playerPrefab, pos, Quaternion.identity);

            //score = loaded.level;
            Debug.Log("Loaded");            
        }      
    }    

    [System.Serializable]
    public class JSON
    {
        public Vector3 JSONpos;        
    }



}
