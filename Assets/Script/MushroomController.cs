using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : Singleton<MushroomController>, ConsumableInterface
{
    public Rigidbody2D mushroomBody;
    private int moveRight = -1;
    private Vector2 velocity;
    // private bool collected;
    private  Vector3 scaler;
    public GameConstants gameConstants;
    public Texture t;
    public SpriteRenderer mushroomSprite;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        velocity = new Vector2(4, 0);
        mushroomBody.AddForce(Vector2.up * 9, ForceMode2D.Impulse);
        scaler = transform.localScale  * (float) gameConstants.spawnTimeStep;
        mushroomSprite = GetComponent<SpriteRenderer>();
    }

    override public void Awake(){
		base.Awake();
		Debug.Log("awake called");
		// other instructions...
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
            mushroomBody.isKinematic = true;
            Destroy(mushroomBody);
            mushroomSprite.enabled = false;
            // Destroy(gameObject); // Destroy gameObject if power has been used, disable sprite renderer and disable rigid body

        }
    }

    IEnumerator  ScaleOut(){
        // NOT IN USE
        for (int step =  0; step  < gameConstants.breakTimeStep; step++)
        {
            this.transform.localScale = this.transform.localScale + scaler;
            // wait for next frame
            yield  return  null;
        }

        Destroy(gameObject);
    }


    // void OnBecameInvisible() {
    //     Destroy(gameObject);
    // }

    public void consumbedBy(GameObject player){
        // give player jump boost
        Debug.Log("MAX SPEED BEFORE CONSUMED: " + player.GetComponent<Lab2PlayerController>().maxSpeed);
        player.GetComponent<Lab2PlayerController>().maxSpeed *= 2;
        Debug.Log("MAX SPEED AFTER CONSUMED: " + player.GetComponent<Lab2PlayerController>().maxSpeed);
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player){
        yield return new WaitForSeconds(5.0f);
        Debug.Log("Removing effect");
        player.GetComponent<Lab2PlayerController>().maxSpeed /= 2;
        Debug.Log("MAX SPEED AFTER 5S: " + player.GetComponent<Lab2PlayerController>().maxSpeed);
        Destroy(gameObject);
    }
}
