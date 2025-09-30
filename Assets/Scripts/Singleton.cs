using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    private static Singleton instance;

    public static Singleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Singleton>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("Singleton");
                    instance = singletonObject.AddComponent<Singleton>();
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as Singleton;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
