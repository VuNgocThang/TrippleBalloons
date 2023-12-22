using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject parent;
    public static ObjectPool Instance;
    public List<ParticleSystem> pooledObjects;
    public ParticleSystem prefab;
    public int amountPrefab;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        pooledObjects = new List<ParticleSystem>();
        ParticleSystem tmp;
        for (int i = 0; i < amountPrefab; i++)
        {
            tmp = Instantiate(prefab, parent.transform);
            tmp.gameObject.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public ParticleSystem GetPooledObject()
    {
        for (int i = 0; i < amountPrefab; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        ParticleSystem tmp = Instantiate(prefab, parent.transform);
        tmp.gameObject.SetActive(false);
        pooledObjects.Add(tmp);
        return tmp;
    }
}
