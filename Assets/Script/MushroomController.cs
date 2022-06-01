using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomController : MonoBehaviour
{
    public Rigidbody2D mushroomBody;
    public float speed;
    public float maxSpeed;
    private int moveRight = -1;
    private Vector2 velocity;

    // Start is called before the first frame update
    void Start()
    {
        mushroomBody = GetComponent<Rigidbody2D>();
        velocity = new Vector2(4, 0);
        mushroomBody.AddForce(Vector2.up * 9, ForceMode2D.Impulse);
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
        }
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
