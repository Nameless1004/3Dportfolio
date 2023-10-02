using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    [SerializeField]
    private bool dontDestroy = true;
    protected bool initialized = false;
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
                // ToDo: 바꿨는데 오류 생길 수 있음
                instance.Init();
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
