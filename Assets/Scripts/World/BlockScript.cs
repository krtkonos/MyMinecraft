using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class BlockScript : MonoBehaviour
{
    public int currentHealth = 20;
    public int maxHealth = 20;
    public AudioSource sound;
    Inventory inv;
    public int id;
    public Vector3 blockPos;


    void Awake()
    {
        SaveSystem.blocks.Add(this);
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
        SaveSystem.blocks.Remove(this);
        //Debug.Log("Destroy saved");
    }
    
    
}
