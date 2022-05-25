using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviour is the base class for all game objects
public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D marioBody;
    public float maxSpeed = 10;
    public float upSpeed = 24;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private bool onGroundState = true;
    public Transform enemyLocation;
    public Text scoreText;
    private int score = 0;
    private bool countScoreState = false;
    private int scoreFinal;
    public Text scoreFinalText;
    public Text gameOverText;
    public Button playAgainButton;
    private Vector2 marioOrigPos;

    void Start()
    {
        	// Set to be 30 FPS
            Application.targetFrameRate =  30;
            marioBody = GetComponent<Rigidbody2D>();
            // Instantiate the marioSprite component 
            marioSprite = GetComponent<SpriteRenderer>();
            scoreFinalText.gameObject.SetActive(false);
            gameOverText.gameObject.SetActive(false);
            playAgainButton.gameObject.SetActive(false);
            marioOrigPos = transform.position;            
    }

     // Updates that have nothing to do with the Physics engine
    void Update()
    {
        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
        }

       //  Debug.Log("Distance Apart: " + Mathf.Abs(transform.position.x - enemyLocation.position.x));

        // When jumping, gomba is near mario and we haven't registered our score
        if (!onGroundState && countScoreState){
            if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f) {
                countScoreState = false;
                score++;
                Debug.Log(score);
            }
        }
    }


    // Update is called once per frame
    void  FixedUpdate()
    {
        // dynamic rigidBody
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(moveHorizontal) > 0) {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);
            if (marioBody.velocity.magnitude < maxSpeed){
                marioBody.AddForce(movement * speed);
            }
        }

        if ((Input.GetKeyUp("a") || Input.GetKeyUp("d")) && onGroundState){
          // stop
          marioBody.velocity = Vector2.zero;
        }

        // To make sure score does not keep updating as mario can't double jump
        if (Input.GetKeyDown("space") && onGroundState){
          marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
          onGroundState = false;
          // marioAnimator.SetBool("OnGround").
          countScoreState = true; //check if Gomba is underneath
        }
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Ground")){
            onGroundState = true; // back on ground
            countScoreState = false; // reset score state
            scoreText.text = "SCORE: " + score.ToString();
        } 

    }

    void OnTriggerEnter2D(Collider2D other){

        if (other.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Gomba!");
            Time.timeScale = 0;
            scoreFinal = score;
            scoreFinalText.text = "FINAL SCORE: " + score.ToString();
            scoreFinalText.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
            playAgainButton.gameObject.SetActive(true);
            scoreText.gameObject.SetActive(false);
            score = 0;
            scoreText.text = "SCORE: " + score.ToString();
            // transform.position = marioOrigPos;
        }
    }
}
