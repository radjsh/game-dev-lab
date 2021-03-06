using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBrick : MonoBehaviour
{
    private bool broken = false;
    public GameObject debris;
    public GameConstants gameConstants;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void  OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player") && !broken){
            Debug.Log("Collided with brick");
            broken = true;
            // assume we have 5 debris per box
            Debug.Log("SPAWN NUMBER OF DEBRIS: " + gameConstants.spawnNumberOfDebris);
            for (int x = 0; x< gameConstants.spawnNumberOfDebris; x++){
                Instantiate(debris, transform.position, Quaternion.identity);
            }
            gameObject.transform.parent.GetComponent<SpriteRenderer>().enabled  =  false;
            gameObject.transform.parent.GetComponent<BoxCollider2D>().enabled  =  false;
            GetComponent<EdgeCollider2D>().enabled  =  false;
        }
}
}
