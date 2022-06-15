using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjectType {
    gombaEnemy = 0,
    greenEnemy = 1
}

[System.Serializable]
public class ObjectPoolItem {
    // System.Serializable allows a class to be visible and customisable in the inspector when declared as a public instance
    public int amount;
    public GameObject prefab;
    public bool expandPool;
    public ObjectType type;
}

public class ExistingPoolItem {
    public GameObject gameObject;
    public ObjectType type;

    // Constructor
    public ExistingPoolItem(GameObject gameObject, ObjectType type){
        //reference input
        this.gameObject = gameObject;
        this.type = type;
    }
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool; // types of different objects to pool
    public List<ExistingPoolItem> pooledObjects; // a list of all objects in the pool, of all types
     // only one instance per scene
    

    void Awake()
    {
        // Access the ObjectPooler instance fast and since there should only be one instance of this per scene,
        // Create one instance of this per scene: 
        SharedInstance = this;
        pooledObjects = new List<ExistingPoolItem>();
        Debug.Log("ObjectPooler is Awake");

        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amount ; i++)
            {
                // this 'pickup' a local variable, but Unity will not remove it since it exists in the scene
                Debug.Log("Instantiating: " + item.type);
                GameObject pickup = (GameObject)Instantiate(item.prefab);
                pickup.SetActive(false);
                pickup.transform.parent = this.transform;

                // e contains a reference to a newly instantiated ExistingPoolItem object 
                ExistingPoolItem e = new ExistingPoolItem(pickup, item.type);
                pooledObjects.Add(e);
            }
        }
    }

    public GameObject GetPooledObject(ObjectType type){
        // return inactive pooled object if it matches the type
        for (int i = 0; i < pooledObjects.Count; i++){
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].type == type){
                return pooledObjects[i].gameObject;
            }
        }

        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.type == type) 
            {
                if (item.expandPool) 
                {
                    GameObject pickup = (GameObject)Instantiate(item.prefab);
                    pickup.SetActive(false);
                    pickup.transform.parent = this.transform;
                    pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
                    return pickup;
                }
            }
        }
        //  will return null IF and only IF the type doesn't match with what is defined in the itemsToPool.
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
