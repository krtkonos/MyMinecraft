using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int BlockrayDistance = 5;
    public Transform camPoint;

    public Transform Highlighted;
    public Transform Place;
    public GameObject Place2;

    public int damagePower = 10;
    public float checkIncrement = 0.1f;
    public float reach = 8f;       
    [SerializeField] private LayerMask mask;



    //public Text blockText;
    // Start is called before the first frame update
    void Start()
    {
        
               
    }

    // Update is called once per frame
    void Update()
    {        
        placeBlock();
        if (Input.GetMouseButtonDown(0))
        {
            Destroy();            
        }
    }
    private void placeBlock()
    {
        float step = checkIncrement;
        Vector3 lastPos = new Vector3();
        //Ray ray = new Ray(camPoint.position, Input.mousePosition * BlockrayDistance);
        RaycastHit hit;
        Physics.Raycast(camPoint.transform.position, camPoint.transform.forward, out hit, BlockrayDistance, mask);
        Debug.DrawRay(camPoint.transform.position, camPoint.transform.forward, Color.red);        

        while (step < reach)
        {            
            
        Vector3 pos = camPoint.position + (camPoint.forward * step);
        lastPos = new Vector3(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));            
        if (hit.collider != null)
        {            
            //Highlighted.position = new Vector3(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y), Mathf.FloorToInt(pos.z));
            Place.transform.position = lastPos;
                

            //Highlighted.gameObject.SetActive(true);
            //Place.gameObject.SetActive(true);
            Debug.Log("ray is ok");
        }
        step += checkIncrement;

        }
        //Highlighted.gameObject.SetActive(false);
        Place.gameObject.SetActive(false);
        

    }
    private void Destroy()
    {        
        RaycastHit hit;
        Physics.Raycast(camPoint.transform.position, camPoint.transform.forward, out hit, BlockrayDistance, mask);                
        Debug.Log("attack is ready");

        if (hit.collider != null)
        {
            BlockScript blc = hit.transform.GetComponent<BlockScript>();
            blc.TakeDamage(damagePower);
        }
        else
            return;
    }
    
}
