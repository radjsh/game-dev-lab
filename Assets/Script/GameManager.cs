using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Singleton Pattern
    private static GameManager _instance;
    // Getter
    public static GameManager Instance {
        // static keyword allows us to gain access to the instance via the Class name
        // there will only be one instance that can be linked to the variable Instance at any given time
        get { return _instance; } 
    }

    public Text score;
    private int playerScore = 0;

    public void increaseScore(){
        playerScore += 1;
        score.text = "SCORE: " + playerScore.ToString();
        onIncreaseScore();
    }

    public void damagePlayer(){
        onPlayerDeath();
    }

    public void spawnMoreEnemy(){
        onSpawnEnemy();
    }

    public delegate void gameEvent();
    public static event gameEvent onPlayerDeath;

    public static event gameEvent onIncreaseScore;
    public static event gameEvent onSpawnEnemy;

}
