using UnityEngine;
using System.Collections.Generic;

// we are only using object pool for GameObjects anyways
public class ObjectPool {

    private List<GameObject> pool;
    private GameObject prefab;
    private int poolSize;

    public ObjectPool() {
        pool = new List<GameObject>();
    }

    public ObjectPool(GameObject obj) {
        pool = new List<GameObject>();
        prefab = obj;
    }

    public void SetPrefab(GameObject obj) {
        prefab = obj;
    }
    
    public GameObject GetObject() {
        while (pool.Count != 0) {
            GameObject obj = pool[pool.Count - 1];
            if (obj == null) {
                pool.RemoveAt(pool.Count - 1);
                continue;
            }
            pool.RemoveAt(pool.Count - 1);
            return obj;
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