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
        playerPosition = new float[3];
        playerPosition[0] = player.transform.position.x;
        playerPosition[1] = player.transform.position.y;
        playerPosition[2] = player.transform.position.z;
        
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
