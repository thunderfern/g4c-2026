using UnityEngine;
using System.Collections.Generic;

// we are only using object pool for gameobjects anyways
public class ObjectPool {

    private List<GameObject> pool;
    private GameObject prefab;
    private int poolSize;

    public ObjectPool(GameObject obj) {
        pool = new List<GameObject>();
        prefab = obj;
    }
    
    public GameObject GetObject() {
        if (pool.Count != 0) {
            GameObject obj = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
        }
        poolSize++;
        return Object.Instantiate(prefab);
    }
    
    public void ReturnObject(GameObject obj) {
        obj.SetActive(false);
        pool.Add(obj);
    }

    public int PoolSize() {
        return poolSize;
    }


}