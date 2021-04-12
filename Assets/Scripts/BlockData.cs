using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockData
{
    public int currentHealth;
    public int id;
    public float[] position;
    public float[] playerPosition;
    public float[] colliderPosition;
    public BlockData(BlockScript block)
    {
        currentHealth = block.currentHealth;
        id = block.id;
        Vector3 blockPos = block.transform.position;
        position = new float[]
        {
            blockPos.x, blockPos.y, blockPos.z
        };

    }
    public BlockData(MyController player)
    {        
        Vector3 playPos = player.transform.position;
        playerPosition = new float[]
        {
            playPos.x, playPos.y, playPos.z
        };
    }
    public BlockData(WorldColliderSCript collid)
    {        
        Vector3 collPos = collid.transform.position;
        colliderPosition = new float[]
        {
            collPos.x, collPos.y, collPos.z
        };
    }
  
    
}
