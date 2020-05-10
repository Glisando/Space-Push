using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerProjectilePool
{
    static GameObject prefabProjectile;
    static List<GameObject> pool;
    static GameObject prefabPlayerProjectileObject;
    static GameObject parentPlayerProjectileObject;
    static bool initialized = false;

    public static bool Initialized
    {
        get { return initialized; }
    }

    public static void Initialize()
    {
        prefabProjectile = Resources.Load<GameObject>("PlayerProjectile");
        prefabPlayerProjectileObject = Resources.Load<GameObject>("PlayerProjectileParentObject");

        parentPlayerProjectileObject = GameObject.Instantiate(prefabPlayerProjectileObject);
        GameObject.DontDestroyOnLoad(parentPlayerProjectileObject);

        pool = new List<GameObject>(2);

        for (int i = 0; i < pool.Capacity; i++)
        {
            pool.Add(GetNewProjectile());
        }
        initialized = true;
    }

    public static GameObject GetProjectile()
    {
        if (pool.Count > 0)
        {
            GameObject projectile = pool[pool.Count - 1];
            pool.RemoveAt(pool.Count - 1);
            return projectile;
        }
        else
        {
            pool.Capacity++;
            return GetNewProjectile();
        }
    }

    public static void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectile.GetComponent<PlayerProjectile>().StopMoving();
        pool.Add(projectile);
    }

    static GameObject GetNewProjectile()
    {
        GameObject projectile = GameObject.Instantiate(prefabProjectile);
        projectile.GetComponent<PlayerProjectile>().Initialize();
        projectile.SetActive(false);
        projectile.transform.parent = parentPlayerProjectileObject.transform;
        GameObject.DontDestroyOnLoad(projectile);
        return projectile;
    }
}
