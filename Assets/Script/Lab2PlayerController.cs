using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// MonoBehaviour is the base class for all game objects
public class Lab2PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Rigidbody2D marioBody;
    public float maxSpeed = 10;
    public float upSpeed = 32;
    private SpriteRenderer marioSprite;
    private bool faceRightState = true;
    private bool onGroundState = true;
    private Vector2 marioOrigPos;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    public ParticleSystem dustCloud;

    void Start()
    {
        	// Set to be 30 FPS
            Application.targetFrameRate =  30;
            marioBody = GetComponent<Rigidbody2D>();
            // Instantiate the marioSprite component 
            marioSprite = GetComponent<SpriteRenderer>();
            marioOrigPos = transform.position;   
            marioAnimator = GetComponent<Animator>();
            marioAudio = GetComponent<AudioSource>();
            marioAnimator.SetBool("onGround", onGroundState);
    }

     // Updates that have nothing to do with the Physics engine
    void Update()
    {
        // Set Animator's 'xSpeed' to match Mario's current speed 
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));

        if (Input.GetKeyDown("a") && faceRightState){
            faceRightState = false;
            marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !faceRightState){
            faceRightState = true;
            marioSprite.flipX = false;
        }

        if ((Input.GetKeyDown("d") || Input.GetKeyDown("a")) && onGroundState){
            if (Mathf.Abs(marioBody.velocity.x) >=  20) {
                marioAnimator.SetTrigger("onSkid");
                Debug.Log("SKIDDING");
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
            //marioAnimator.SetTrigger("onSkid");
        }

        // To make sure score does not keep updating as mario can't double jump
        if (Input.GetKeyDown("space") && onGroundState){
          marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
          onGroundState = false;
          marioAnimator.SetBool("onGround", onGroundState);
        }
    }

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Ground") || col.gameObject.CompareTag("Obstacle")){
            onGroundState = true; // back on ground
            marioAnimator.SetBool("onGround", onGroundState);
            dustCloud.Play();
        } 
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy")){
            Debug.Log("Collided with Gomba!");
            Time.timeScale = 0;
        }
    }

    void PlayJumpSound(){
	    marioAudio.PlayOneShot(marioAudio.clip);
    }
}
