using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : Component
{
    public GameObject parent;
    public static ObjectPool<T> Instance;
    public List<T> pooledObjects;
    public T prefab;
    public int amountPrefab;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledObjects = new List<T>();
        T tmp;
        for (int i = 0; i < amountPrefab; i++)
        {
            tmp = Instantiate(prefab, parent.transform);
            tmp.gameObject.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public T GetPooledObject()
    {
        for (int i = 0; i < amountPrefab; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        T tmp = Instantiate(prefab, parent.transform);
        tmp.gameObject.SetActive(false);
        pooledObjects.Add(tmp);
        return tmp;
    }
}
