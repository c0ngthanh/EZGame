using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    public static PoolObject SharedInstance;
    [HideInInspector] public List<GameObject> pooledObjects;
    public List<GameObject> objectToPool;
    public int amountToPool;

    void Awake()
    {
        if (SharedInstance == null)
        {
            SharedInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (SharedInstance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            for(int j = 0; j < objectToPool.Count; j++)
            {
                tmp = Instantiate(objectToPool[j]);
                tmp.SetActive(false);
                tmp.transform.SetParent(transform);
                pooledObjects.Add(tmp);
            }
        }
    }
    public GameObject GetPooledObject(Faction faction)
    {
        for(int i = 0; i < amountToPool*objectToPool.Count; i++)
        {
            if(!pooledObjects[i].activeInHierarchy && pooledObjects[i].GetComponent<Unit>().faction == faction)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    public void ReturnPooledObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }
}
