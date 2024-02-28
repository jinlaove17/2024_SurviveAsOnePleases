using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> where T : new()
{
    public static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }

            return _instance;
        }
    }
}

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);

                    _instance = obj.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
}
