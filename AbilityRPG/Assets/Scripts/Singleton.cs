using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://ronniej.sfuh.tk/unity3d-%EC%A0%9C%EB%84%A4%EB%A6%ADgeneric%EC%9D%84-%EC%A0%81%EC%9A%A9%ED%95%9C-%EC%8B%B1%EA%B8%80%ED%86%A4-%EB%A7%A4%EB%8B%88%EC%A0%B8-%EB%A7%8C%EB%93%A4%EA%B8%B0/
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                    Debug.LogError("There's no active " + typeof(T) + " in this scene");
            }

            return _instance;
        }
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
