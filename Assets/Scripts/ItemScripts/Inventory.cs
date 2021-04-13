using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Items database;
    int maxAmount = 64;
    int sloteAmount = 9;
    int storageAmount = 36;
    public GameObject slot;
    public GameObject invItem;
    public int hoverIndex;
    public GameObject hotbarPanel;
    public GameObject inventoryPanel;

    public List<Item> items = new List<Item>();
    public List<GameObject> slots = new List<GameObject>();
        
    // Start is called before the first frame update
    void Start()
    {
        database = gameObject.GetComponent<Items>();
        for (int i = 0; i < sloteAmount; i++)
        {
            items.Add(database.GetItemByID(-1));
            slots.Add(Instantiate(slot));
            slots[i].GetComponent<SlotScript>().slotNumber = i;
            slots[i].transform.SetParent(hotbarPanel.transform);
            slots[i].GetComponent<RectTransform>().transform.localScale = Vector3.one;
        }
        for (int i = sloteAmount; i < storageAmount; i++)
        {
            items.Add(database.GetItemByID(-1));
            slots.Add(Instantiate(slot));
            slots[i].GetComponent<SlotScript>().slotNumber = i;
            slots[i].transform.SetParent(inventoryPanel.transform);
            slots[i].GetComponent<RectTransform>().transform.localScale = Vector3.one;
        }

        AddItem(0);
        AddItem(0);
        AddItem(0);
        AddItem(1);
        ToggleInventory();
    }

    private void Update()
    {
        for (int i = 0; i < sloteAmount; i++)
        {
            if ( i == hoverIndex)
            {
                slots[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                slots[i].GetComponent<Image>().color = slot.GetComponent<Image>().color;
            }
        }
    }
    public void RemoveItem() 
    {
        for (int i = 0; i < sloteAmount; i++)
        {
            if (i == hoverIndex && items[i].ID != -1) 
            {
                ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                data.amount--;
                if (data.amount <= 0)
                {
                    Destroy(slots[i].transform.GetChild(0).gameObject);
                    items[i] = database.GetItemByID(-1);
                }
                data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                break;
            }
        }
    }

    public void AddItem(int id)
    {
        Item itemToAdd = database.GetItemByID(id);
        if (itemToAdd.Stackable && CheckInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    if (slots[i].transform.GetChild(0).GetComponent<ItemData>().amount < maxAmount) 
                    {
                        ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                        data.amount++;
                        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                if(items[i].ID == -1)
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(invItem);
                    itemObj.GetComponent<ItemData>().item = itemToAdd;
                    itemObj.GetComponent<ItemData>().curSlot = i;
                    itemObj.transform.SetParent(slots[i].transform);
                    itemObj.name = itemToAdd.Name;
                    itemObj.transform.localScale = Vector3.one;
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;
                    break;
                }
            }
        }
    }
    bool CheckInventory(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if(items[i].ID == item.ID)
            {
                if (slots[i].transform.GetChild(0).GetComponent<ItemData>().amount < maxAmount)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);        
    }
}
