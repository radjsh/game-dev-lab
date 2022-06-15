using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
