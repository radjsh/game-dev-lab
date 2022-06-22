using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T _instance;
    public static T instance{
        get{
            return _instance;
        }
    }

    // virtual method allows override by members inheriting this class, and the members can utilise this base Singleton class 
    public virtual void Awake(){
        Debug.Log("Singleton Awake called");

        if (_instance == null){
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }
    }

}
