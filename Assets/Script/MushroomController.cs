using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour, ConsumableInterface
{
    public Rigidbody2D mushroomBody;
    private int moveRight = -1;
    private Vector2 velocity;
    // private bool collected;
    private  Vector3 scaler;
    public GameConstants gameConstants;
    public Texture t;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        velocity = new Vector2(4, 0);
        mushroomBody.AddForce(Vector2.up * 9, ForceMode2D.Impulse);
        scaler = transform.localScale  * (float) gameConstants.spawnTimeStep;
    }

    void MoveMushroom(){
        mushroomBody.MovePosition(mushroomBody.position + (moveRight) * velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveMushroom();
    }

    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Obstacle")){
            moveRight *= -1;
        } 
        if (col.gameObject.CompareTag("Player")){
            velocity = Vector2.zero;
            // collected = true;
            //this.transform.localScale = new Vector3(1.2f, 1.2f, 0);
            //StartCoroutine(ScaleOut());
            CentralManager.centralManagerInstance.addPowerup(t, 1, this);
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);

        }
    }

    IEnumerator  ScaleOut(){
        for (int step =  0; step  < gameConstants.breakTimeStep; step++)
        {
            this.transform.localScale = this.transform.localScale + scaler;
            // wait for next frame
            yield  return  null;
        }

        Destroy(gameObject);
    }


    void OnBecameInvisible() {
        Destroy(gameObject);
    }

    public void consumbedBy(GameObject player){
        // give player jump boost
        player.GetComponent<Lab2PlayerController>().maxSpeed *= 2;
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player){
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<Lab2PlayerController>().maxSpeed /= 2;
    }
}
