using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform marioPosition;
    // Start is called before the first frame update
    void Start()
    {
        GameManager.onIncreaseScore += spawnEnemy;
        for (int j =  0; j  <  2; j++){
	        spawnFromPooler(ObjectType.gombaEnemy);
        }
        GameManager.onSpawnEnemy += spawnEnemy;
    }

    void spawnFromPooler(ObjectType i){
        // static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null){
            // Set position & other necessary states
            item.transform.position = new Vector3(Random.Range(-4.5f + marioPosition.position.x, 4.5f + marioPosition.position.x), item.transform.position.y, 0);
            item.SetActive(true);
        }
        else {
            Debug.Log("not enough items in the pool");
        }
    }

    void spawnEnemy(){
        var enemyType = Random.Range(0, 2);
        if (enemyType == 0){
            spawnFromPooler(ObjectType.greenEnemy);
        } else {
            spawnFromPooler(ObjectType.gombaEnemy);
        }
        
    }
}
