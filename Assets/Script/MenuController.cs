using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public Transform marioPosition;
    public Transform enemyPosition;
    private Vector2 marioOrigPos;
    private Vector2 enemyOrigPos;
    // Prevent the game from starting before the 'START' button is pressed
    void Awake() {
        // Time scale of the game to be 0 in the beginning
        Time.timeScale = 0.0f;
        marioOrigPos = marioPosition.position;
        enemyOrigPos = enemyPosition.position;
    }

    public void StartButtonClicked(){
        // Iterate through the children of the UI and disable them so they're not rendered on the Scene anymore
        foreach (Transform eachChild in transform) {
            if (eachChild.name == "ScoreText"){
                eachChild.gameObject.SetActive(true);
            }
            if (eachChild.name != "ScoreText"){
                Debug.Log("Child found. Name: " + eachChild.name);
                // disable them
                eachChild.gameObject.SetActive(false);
                // Time scale of the game to be 1 after button is pressed
                Time.timeScale = 1.0f;
            }
        }
        ResetPosition();
    }

    public void ResetPosition(){
        marioPosition.position = marioOrigPos;
        enemyPosition.position = enemyOrigPos;
    }
}
