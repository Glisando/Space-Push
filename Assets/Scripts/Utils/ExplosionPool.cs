using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExplosionPool
{
    static List<GameObject> pool;
    static GameObject explosionPrefab;
    static GameObject prefabExplosionsObject;
    static GameObject parentExplosionsObject;
    static bool initialized = false;

    public static bool Initialized
    {
        get { return initialized; }
    }

    public static void Initialize()
    {
        explosionPrefab = Resources.Load<GameObject>("Explosion");
        prefabExplosionsObject = Resources.Load<GameObject>("ExplosionsParentObject");

        parentExplosionsObject = GameObject.Instantiate(prefabExplosionsObject);
        GameObject.DontDestroyOnLoad(parentExplosionsObject);

        pool = new List<GameObject>(2);

        for (int i = 0; i < pool.Capacity; i++)
        {
            pool.Add(GetNewExplosion());
        }

        initialized = true;
    }

    public static GameObject GetExplosion()
    {
        if (pool.Count > 0)
        {
            GameObject explosion = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return explosion;
        }
        else
        {
            pool.Capacity++;
            return GetNewExplosion();
        }
    }

    public static void ReturnExplosion(GameObject explosion)
    {
        explosion.GetComponent<Explosion>().Stop();
        explosion.SetActive(false);
        pool.Add(explosion);
    }

    static GameObject GetNewExplosion()
    {
        GameObject explosion = GameObject.Instantiate(explosionPrefab);
        explosion.GetComponent<Explosion>().Initialize();
        explosion.SetActive(false);
        explosion.transform.parent = parentExplosionsObject.transform;
        GameObject.DontDestroyOnLoad(explosion);
        return explosion;
    }
}
