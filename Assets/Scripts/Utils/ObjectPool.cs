using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [System.Serializable]
    public struct Pool
    {
        public Queue<GameObject> pooledObjects;
        public GameObject objectPrefab;
        public int poolSize;
    }
    public Pool[] pools = null;
    void Awake()
    {
        for (int j = 0; j < pools.Length; j++)
        {
            pools[j].pooledObjects = new Queue<GameObject>();
            for (int i = 0; i < pools[j].poolSize; i++)
            {
                GameObject obj = Instantiate(pools[j].objectPrefab);
                obj.gameObject.SetActive(false);

                pools[j].pooledObjects.Enqueue(obj);
            }
        }
    }
    public GameObject GetPooledObject(int objectType)
    {
        if(objectType >= pools.Length)
            return null;
        GameObject obj = pools[objectType].pooledObjects.Dequeue();
        obj.gameObject.SetActive(true);

        // pools[objectType].pooledObjects.Enqueue(obj);
        return obj;
    }

}


