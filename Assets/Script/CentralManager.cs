using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
    private GameManager gameManager;
    public static CentralManager centralManagerInstance;
    public GameObject powerupManagerObject;
    private PowerUpManager powerUpManager;

    // Start is called before the first frame update
    void Awake()
    {
        centralManagerInstance = this;
    }

    void Start(){
        gameManager = gameManagerObject.GetComponent<GameManager>();
        powerUpManager = powerupManagerObject.GetComponent<PowerUpManager>();
    }

    public void increaseScore(){
        gameManager.increaseScore();
    }

    public void damagePlayer(){
        gameManager.damagePlayer();
    }

    public void spawnEnemy(){
        gameManager.spawnMoreEnemy();
    }

    public void consumePowerup(KeyCode k, GameObject g){
        powerUpManager.consumePowerup(k, g);
    }

    public void addPowerup(Texture t, int i, ConsumableInterface c){
        powerUpManager.addPowerup(t, i, c);
    }

    public void changeScene()
    {
        StartCoroutine(LoadYourAsyncScene("Level2"));
    }

    IEnumerator LoadYourAsyncScene(string sceneName)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

}
