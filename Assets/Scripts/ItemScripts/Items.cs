using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public struct Item
{
    public int ID;
    public string Name;
    public bool Stackable;
    public bool Placeable;
    public string Slug;
    public Sprite Sprite;
    public GameObject Object;

    public Item( int id, string name, bool stackable, bool placeable, string slug)
    {
        ID = id;
        Name = name;
        Stackable = stackable;
        Placeable = placeable;
        Slug = slug;
        Sprite = Resources.Load<Sprite>("Sprites/" + slug);
        Object = Resources.Load<GameObject>("Blocks/" + slug + "Block");
    }
}

public class Items : MonoBehaviour
{
    public List<Item> itemDatabase = new List<Item>();
    // Start is called before the first frame update
    void Start()
    {
        GetDatabase("Assets/Resources/Items.txt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetDatabase(string path)
    {
        StreamReader sr = new StreamReader(path);

        AddItem:
        itemDatabase.Add(new Item(
            int.Parse(sr.ReadLine().Replace("id: ","")),
            sr.ReadLine().Replace("name: ",""),
            bool.Parse(sr.ReadLine().Replace("stackable: ","")),
            bool.Parse(sr.ReadLine().Replace("placeable: ","")),
            sr.ReadLine().Replace("slug: ","")
            ));

        string c = sr.ReadLine();
        if(c == ",")
        {
            goto AddItem;
        }
        else if (c == ";")
        {
            sr.Close();
        }
        else
        {
            Debug.LogError("Item Data does not have correct line ending");
        }
    }
    public Item GetItemByID(int id)
    {
        for (int i = 0; i < itemDatabase.Count; i++)
        {
            if (itemDatabase[i].ID == id)
            {
                return itemDatabase[i];
            }
        }
        return itemDatabase[0];
    }
}
