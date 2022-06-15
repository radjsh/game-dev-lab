using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class redMushroomController : MonoBehaviour, ConsumableInterface
{
    private AudioSource mushroomAudio;

    // Start is called before the first frame update
    void Start()
    {
        mushroomAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.CompareTag("Ground")){
            mushroomAudio.PlayOneShot(mushroomAudio.clip);
        } 

        if (col.gameObject.CompareTag("Player")){
            CentralManager.centralManagerInstance.addPowerup(t, 0, this);
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject);
        } 
    }

    public Texture t;
    public void consumbedBy(GameObject player){
        // give player jump boost
        Debug.Log("POWER UP");
        player.GetComponent<Lab2PlayerController>().upSpeed += 20;
        StartCoroutine(removeEffect(player));
    }

    IEnumerator removeEffect(GameObject player){
        yield return new WaitForSeconds(5.0f);
        player.GetComponent<Lab2PlayerController>().upSpeed -= 20;
    }
}
