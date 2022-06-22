using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redMushroomController : Singleton<redMushroomController>, ConsumableInterface
{
    private AudioSource mushroomAudio;
    public Rigidbody2D redMushroomBody;
    public SpriteRenderer redMushroomSprite;

    // Start is called before the first frame update
    void Start()
    {
        mushroomAudio = GetComponent<AudioSource>();
        redMushroomBody = GetComponent<Rigidbody2D>();
        redMushroomSprite = GetComponent<SpriteRenderer>();
    }

	override public void Awake(){
		base.Awake();
		Debug.Log("awake called");
		// other instructions...
	}

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Ground")){
            mushroomAudio.PlayOneShot(mushroomAudio.clip);
        } 

        if (col.gameObject.CompareTag("Player")){
            CentralManager.centralManagerInstance.addPowerup(t, 0, this);
            GetComponent<Collider2D>().enabled = false;
            redMushroomBody.isKinematic = true;
            Destroy(redMushroomBody);
            redMushroomSprite.enabled = false;
        } 
    }

    public Texture t;
    public void consumbedBy(GameObject player){
        // give player jump boost
        Debug.Log("POWER UP RED MUSHROOM");
        player.GetComponent<Lab2PlayerController>().upSpeed += 20;
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player){
        yield return new WaitForSeconds(5.0f);
        Debug.Log("DESTORYING RED MUSHROOM");
        player.GetComponent<Lab2PlayerController>().upSpeed -= 20;
        Destroy(gameObject);

    }
}
