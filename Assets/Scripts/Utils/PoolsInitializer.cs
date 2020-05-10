using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolsInitializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
	{
        if (!PlayerProjectilePool.Initialized)
            PlayerProjectilePool.Initialize();

        if (!ExplosionPool.Initialized)
            ExplosionPool.Initialize();
    }
}
