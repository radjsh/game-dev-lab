using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ChangeScene : MonoBehaviour
{
	public AudioSource changeSceneSound;

    void Start(){
        changeSceneSound = GetComponent<AudioSource>();
    }

	void OnTriggerEnter2D(Collider2D other)
	{
        Debug.Log("Entering TRIGGER ENTER");

		if (other.tag == "Player")
		{
            Debug.Log("CASTLE COLLIDED WITH MARIO");
			changeSceneSound.PlayOneShot(changeSceneSound.clip);
			StartCoroutine(LoadYourAsyncScene("Level2"));
		}
	}

	IEnumerator  LoadYourAsyncScene(string sceneName)
	{
		yield  return  new  WaitUntil(() =>  !changeSceneSound.isPlaying);
		CentralManager.centralManagerInstance.changeScene();
	}
}
