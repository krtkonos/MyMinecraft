using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BlockScript : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public AudioSource sound;
    Inventory inv;
    public int id;
    public Vector3 blockPos;


    void Awake()
    {
        if (maxHealth == 10)
        {
            SaveSystem.blocks.Add(this);
        }
        else if (maxHealth == 20)
        {
            SaveSystem.dirtB.Add(this);

        }
        else if (maxHealth == 30)
        {
            SaveSystem.stoneB.Add(this);

        }
        else if (maxHealth == 1000)
        {
            SaveSystem.brB.Add(this);
        }
        else { }                
    }
    // Start is called before the first frame update
    void Start()
    {
        inv = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<Inventory>();
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        blockPos = transform.position;
    }
    /*private void OnMouseDown()
    {
        Destroy(this.gameObject);
    }*/
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //Debug.Log("Take damage");
        if(currentHealth <= 0)
        {
            //Debug.Log("Damaged for real");
            Destroyed();
        }

    }
    private void Destroyed()
    {
        inv.AddItem(id);
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        //SaveSystem.blocks.Remove(this);
        if (maxHealth == 10)
        {
            SaveSystem.blocks.Remove(this);
        }
        else if (maxHealth == 20)
        {
            SaveSystem.dirtB.Remove(this);

        }
        else if (maxHealth == 30)
        {
            SaveSystem.stoneB.Remove(this);

        }
        else if (maxHealth == 1000)
        {
            SaveSystem.brB.Remove(this);
        }
        else { }
    }
    
    
}
