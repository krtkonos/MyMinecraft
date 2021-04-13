using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MyController : MonoBehaviour
{
    public float moveSpeed = 6;
    public float jumpSpeed = 8;
    public float gravity = 20;
    Vector3 moveDir = Vector3.zero;
    public static Vector3 pos;
    float yRotation = 0;
    float yRot;
    float xRot;
    public float sensitivity = 4;
    Camera cam;
    public LayerMask mask;
    float blockrayDistance = 5;
    int damagePower = 10;
    public bool canInteract;
    public GameObject world;
    public GameObject collid;
    int ax;
    int az;
    public WorldGenerator wc;
    Inventory inv;
    public Transform transPlaceBlock;
    public List<GameObject> newBlocks;
    

    void Start()
    {
        //Load();
        transPlaceBlock.gameObject.SetActive(false);
        canInteract = true;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        inv = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
        
    }
    /*void Load()
    {
        BlockData data = SavingSystem.LoadPlayer();
        Vector3 position;
        position.x = data.playerPosition[0];
        position.y = data.playerPosition[1];
        position.z = data.playerPosition[2];
        transform.position = position;
    }*/

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10)
        {
            transform.position = new Vector3(0, 50, 0);
        }
        
        pos = transform.position;
        /*if (Input.GetKeyDown("p"))
        {
            Save();
            Debug.Log("saved: " + pos);
        }*/
        //Debug.Log(pos);
        CollidRay();
        ax = (int)transform.position.x;
        az = (int)transform.position.z;

        MovePlayer();
        if (canInteract)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            MouseLook();
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                RaycastHit hit;
                BlockScript block;
                //Destroy();
                if (Physics.Raycast(ray, out hit, blockrayDistance))
                {
                    if (block = hit.transform.GetComponent<BlockScript>())
                    {
                        block.TakeDamage(damagePower);
                    }
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                if (inv.items[inv.hoverIndex].Placeable == true)
                {
                    Ray ray = new Ray(cam.transform.position, cam.transform.forward);
                    RaycastHit hit;
                    //BlockScript block;

                    if (Physics.Raycast(ray, out hit, blockrayDistance))
                    {

                        Vector3 spawnPos = Vector3.zero;

                        float xDiff = hit.point.x - hit.transform.position.x;
                        float yDiff = hit.point.y - hit.transform.position.y;
                        float zDiff = hit.point.z - hit.transform.position.z;

                        if (Mathf.Abs(yDiff) == 0.5f)
                        {
                            spawnPos = hit.transform.position + (Vector3.up * yDiff) * 2;
                        }
                        else if (Mathf.Abs(xDiff) == 0.5f)
                        {
                            spawnPos = hit.transform.position + (Vector3.right * xDiff) * 2;
                        }
                        else if (Mathf.Abs(zDiff) == 0.5f)
                        {
                            spawnPos = hit.transform.position + (Vector3.forward * zDiff) * 2;
                        }

                        //Instantiate(inv.items[inv.hoverIndex].Object, spawnPos, Quaternion.identity);
                        inv.RemoveItem();
                        GameObject bl = Instantiate(inv.items[inv.hoverIndex].Object, spawnPos, Quaternion.identity) as GameObject;
                        bl.transform.SetParent(GameObject.FindWithTag("Enviro").transform);
                        //SaveSystem.blocks.Add(inv.items[inv.hoverIndex].Object);
                        //newBlocks.Add(inv.items[inv.hoverIndex].Object);
                    }
                }
            }
            PlaceCursorBlock();

        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            transPlaceBlock.gameObject.SetActive(false);
        }
    }
    public void PlaceCursorBlock()
    {
        if (inv.items[inv.hoverIndex].Placeable == true)
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);
            RaycastHit hit;
            //BlockScript block;

            if (Physics.Raycast(ray, out hit, blockrayDistance))
            {
                Vector3 spawnPos = new Vector3();

                float xDiff = hit.point.x - hit.transform.position.x;
                float yDiff = hit.point.y - hit.transform.position.y;
                float zDiff = hit.point.z - hit.transform.position.z;

                if (Mathf.Abs(yDiff) == 0.5f)
                {
                    spawnPos = hit.transform.position + (Vector3.up * yDiff) * 2;
                }
                else if (Mathf.Abs(xDiff) == 0.5f)
                {
                    spawnPos = hit.transform.position + (Vector3.right * xDiff) * 2;
                }
                else if (Mathf.Abs(zDiff) == 0.5f)
                {
                    spawnPos = hit.transform.position + (Vector3.forward * zDiff) * 2;
                }
                transPlaceBlock.gameObject.SetActive(true);
                //transPlaceBlock.position = spawnPos;     
                transPlaceBlock.transform.localRotation = Quaternion.identity;
                transPlaceBlock.position = spawnPos;

                //Debug.Log("Placeable");

            }
        }
    }
    void MovePlayer()
    {
        CharacterController character = GetComponent<CharacterController>();

        if (character.isGrounded)
        {
            moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDir = transform.TransformDirection(moveDir);
            moveDir *= moveSpeed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDir.y = jumpSpeed;
            }
        }

        moveDir.y -= gravity * Time.deltaTime;
        character.Move(moveDir * Time.deltaTime);
    }

    void MouseLook()
    {
        yRot = -Input.GetAxis("Mouse Y") * sensitivity;
        xRot = Input.GetAxis("Mouse X") * sensitivity;
        yRotation += yRot;
        yRotation = Mathf.Clamp(yRotation, -80, 80);

        if (xRot != 0)
        {
            transform.eulerAngles += new Vector3(0, xRot, 0);
        }
        if (yRot != 0)
        {
            cam.transform.eulerAngles = new Vector3(yRotation, transform.eulerAngles.y, 0);
        }
    }
    void CollidRay()
    {
        Ray rayUp = new Ray(gameObject.transform.position, gameObject.transform.up);
        Ray rayDown = new Ray(gameObject.transform.position, -gameObject.transform.up);
        RaycastHit hit;
        if (Physics.Raycast(rayUp, out hit, 50, mask) || Physics.Raycast(rayDown, out hit, 50, mask))
        {

        }
        else
        {
            if (transform.position.y > -10)
            {
                GameObject coll = Instantiate(collid, new Vector3(transform.position.x, transform.position.y - 10,
                        transform.position.z), Quaternion.identity);
                coll.transform.SetParent(GameObject.FindWithTag("Enviro").transform);
                wc.GenerateNewTerrain(ax, az);

            }
        }
    }
    public void Save()
    {
        JSON myObject = new JSON();
        // myObject.level = score;
        myObject.JSONpos = transform.position;
        //myObject.newBlock = newBlocks;


        string json = JsonUtility.ToJson(myObject);

        File.WriteAllText(Application.dataPath + SaveSystem.PLAYER_SUB, json);
        Debug.Log(json + "saved");
    }
    public void Load()
    {
        JSON myObject = new JSON();
        string json = File.ReadAllText(Application.dataPath + SaveSystem.PLAYER_SUB);
        JSON loaded = JsonUtility.FromJson<JSON>(json);
        transform.position = loaded.JSONpos;

        //score = loaded.level;
        Debug.Log("Loaded");

    }

    private void OnApplicationQuit()
    {
        //Save();
    }
    
    [Serializable]
    public class JSON
    {
        public Vector3 JSONpos;
        //public List<GameObject> newBlock;
    }
    

}
