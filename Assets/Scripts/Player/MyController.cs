using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MyController : MonoBehaviour
{
    public WorldGenerator wc;
    public List<GameObject> newBlocks;
    Inventory inv;
    public Transform transPlaceBlock;
    public GameObject world;
    public GameObject collid;
    Camera cam;
    Vector3 moveDir = Vector3.zero;
    public static Vector3 pos;
    public float moveSpeed = 6;
    public float jumpSpeed = 8;
    public float gravity = 20;
    float yRotation = 0;
    float yRot;
    float xRot;
    public float sensitivity = 4;
    public LayerMask mask;
    float blockrayDistance = 5;
    int damagePower = 10;
    public bool canInteract;
    int ax;
    int az;
    

    void Start()
    {        
        transPlaceBlock.gameObject.SetActive(false); //placeBlock non active til raycast hit
        canInteract = true; //invenotry off
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked; // no mouse
        inv = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
        
    }
    
    void Update()
    {
        if(transform.position.y < -10)
        {
            transform.position = new Vector3(0, 50, 0);
        }        
        pos = transform.position;
        
        CollidRay();
        ax = (int)transform.position.x;
        az = (int)transform.position.z;

        MovePlayer();
        if (canInteract)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            MouseLook();
            if (Input.GetMouseButtonDown(0)) // sestroy blocks
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
            if (Input.GetMouseButtonDown(1)) // place blocks
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
                        
                        inv.RemoveItem(); // remove from actionbar
                        GameObject bl = Instantiate(inv.items[inv.hoverIndex].Object, spawnPos, Quaternion.identity) as GameObject;
                        bl.transform.SetParent(GameObject.FindWithTag("Enviro").transform);     //organize                   
                    }
                }
            }
            PlaceCursorBlock();

        }
        else // inventory opened
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
                     
                transPlaceBlock.transform.localRotation = Quaternion.identity;
                transPlaceBlock.position = spawnPos;
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
    void CollidRay() // check if should generate new world part
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
        
        myObject.JSONpos = transform.position;        

        string json = JsonUtility.ToJson(myObject);
        File.WriteAllText(Application.dataPath + SaveSystem.PLAYER_SUB, json);
        Debug.Log(json + "saved");
    }
    
    
    
    [Serializable]
    public class JSON
    {
        public Vector3 JSONpos;
    }
    

}
