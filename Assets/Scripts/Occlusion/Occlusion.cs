using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Occlusion : MonoBehaviour
{
    Renderer myRend;
    public float timeDisplay;

    private void OnEnable()
    {
        myRend = gameObject.GetComponent<Renderer>();
        timeDisplay = -1;
    }
    private void Update()
    {
        if(timeDisplay > 0)
        {
            timeDisplay -= Time.deltaTime;
            myRend.enabled = true;
        }
        else
        {
            myRend.enabled = false;
        }
    }
    public void HitOclude(float time)
    {
        timeDisplay = time;
        myRend.enabled = true;
    }
}
