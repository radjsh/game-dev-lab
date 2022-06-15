using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consummablePrefab; // the spawned mushroom prefab
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox; // the sprite that indicates empty box instead
    private bool hit = false;
    private AudioSource questionAudio;
    //public AudioSource questionBoxAudio;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        springJoint = GetComponent<SpringJoint2D>();
        // consummablePrefab = GetComponent<GameObject>();
        // spriteRenderer = GetComponent<SpriteRenderer>();
        // usedQuestionBox = GetComponent<Sprite>();
        questionAudio = GetComponent<AudioSource>();
    }
    
    void  OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") &&  !hit){
            hit  =  true;
            questionAudio.PlayOneShot(questionAudio.clip);
            // ensure that we move the box sufficiently
            rigidBody.AddForce(new Vector2(0, rigidBody.mass*10), ForceMode2D.Impulse);
            // spawn the mushroom prefab slightly above the box
            Instantiate(consummablePrefab, new  Vector3(this.transform.position.x, this.transform.position.y  +  1.0f, this.transform.position.z), Quaternion.identity);
            StartCoroutine(DisableHittable());
            CentralManager.centralManagerInstance.spawnEnemy();
        }
    }

    bool ObjectMovedAndStopped(){
        return Mathf.Abs(rigidBody.velocity.magnitude) < 0.01;
    }

    IEnumerator DisableHittable(){
        if (!ObjectMovedAndStopped()){
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        Debug.Log("OBJECT GOT HIT");

        // continues here when the ObjectMovedAndStopped() returns true
        spriteRenderer.sprite = usedQuestionBox; // change sprite
        rigidBody.bodyType = RigidbodyType2D.Static; // make the box unaffected by physics

        // reset box position
        this.transform.localPosition = new Vector3(0, -0.3f, 0);
        springJoint.enabled = false; // disable spring
    }
}
