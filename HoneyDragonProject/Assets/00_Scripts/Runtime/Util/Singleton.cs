using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    [SerializeField]
    private bool dontDestroy = true;
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if(instance is null)
            {
                instance = FindObjectOfType<T>(); ;
                if(instance is null)
                {
                    GameObject go = new GameObject(nameof(T));
                    instance = go.AddComponent<T>();
                }

                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }

    private void Awake()
    {
        if(instance is null)
        {
            if(dontDestroy)
            {
                Instance.Init();
            }
            else
            {
                instance = GetComponent<T>();
                Init();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected abstract void Init();
}
