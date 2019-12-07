using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T: Singleton<T>
{
    private static T _instance;
    public static T Instance => _instance;

    public static bool IsInitialized => _instance != null;

    // protected : method can be called by class that extend this class
    // virtual: method can be ovveride by class that extend this class
    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Debug.LogError("[Singleton] Trying to instantiate a second instance of singleton class");
            Destroy(gameObject);
        }
        else
        {
            _instance = (T) this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}
