using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    Inventory inv;
    MyController player;
    float itemIndex;
   
    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();        
        player = gameObject.GetComponent<MyController>();        
    }

    // Update is called once per frame
    void Update()
    {        
        CheckKeys();
    }
    void CheckKeys() // chose from hotbar
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            itemIndex = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            itemIndex = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            itemIndex = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            itemIndex = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            itemIndex = 4;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            itemIndex = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            itemIndex = 6;
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            itemIndex = 7;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            itemIndex = 8;
        }

        itemIndex -= Input.GetAxis("Mouse ScrollWheel");
        if (itemIndex < 0)
        {
            itemIndex = 0;
        }
        else if(itemIndex > 8)
        {
            itemIndex = 0;
        }
        inv.hoverIndex = (int)itemIndex;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inv.ToggleInventory();
            player.canInteract = !player.canInteract;
        }
    }
}
