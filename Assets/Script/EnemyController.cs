using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float originalX;
    private int moveRight;
    private Vector2 velocity;
    private Rigidbody2D enemyBody;
    public GameConstants gameConstants;
    private AudioSource enemyAudio;
    private SpriteRenderer enemySprite;
    // public static event gameEvent onPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        enemyBody = GetComponent<Rigidbody2D>();
        // get the starting position
        originalX = transform.position.x;
        moveRight = Random.Range(0, 2) == 0 ? -1 : 1;
        ComputeVelocity();
        // subscribe to player event
        GameManager.onPlayerDeath += EnemyRejoice;
        enemyAudio = GetComponent<AudioSource>();
        enemySprite = GetComponent<SpriteRenderer>();
    }

    void ComputeVelocity(){
        velocity = new Vector2((moveRight)* gameConstants.maxOffset / gameConstants.enemyPatroltime, 0);
    }

    void MoveEnemy(){
        enemyBody.MovePosition(enemyBody.position + velocity * Time.fixedDeltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(enemyBody.position.x - originalX) < gameConstants.maxOffset){
            // move gomba
            MoveEnemy();
        } else {
            // change direction
            moveRight *= -1;
            enemySprite.flipX = !enemySprite.flipX;
            ComputeVelocity();
            MoveEnemy();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Player")){
            // check if collides on top
            float yoffset = (other.transform.position.y - this.transform.position.y);
            if (yoffset > 0.75f){
                KillSelf();
            }
            else {
                CentralManager.centralManagerInstance.damagePlayer();
            }
        }

        if (other.gameObject.CompareTag("Obstacle")){
            Debug.Log("Goomba collided with pipe");
            moveRight *= -1;
            ComputeVelocity();
            MoveEnemy();
        } 
    }

    void KillSelf(){
        // enemy dies
        CentralManager.centralManagerInstance.increaseScore();
        StartCoroutine(flatten());
        Debug.Log("Kill Sequence Ends");
    }

    IEnumerator flatten(){
        Debug.Log("Flatten Starts");
        int steps = 5;
        float stepper = 1.0f / (float) steps;
        for (int i = 0; i < steps; i++){
            this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

            // make sure enemy is still above ground
            this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
            yield return null;
        }

        Debug.Log("Flatten Ends");
        this.gameObject.SetActive(false);
        Debug.Log("Enemy returned to pool");
        yield break;
    }

    void EnemyRejoice(){
        Debug.Log("Enemey killed Mario");
        // add animation and sound
        Debug.Log("PLAYING AUDIO");
        enemyAudio.PlayOneShot(enemyAudio.clip);
        // this.transform.position = new Vector3(0, this.transform.position.y + 1f, 0);
    }
}
