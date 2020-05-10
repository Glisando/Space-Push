using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTimer : Timer
{
    public static event Action EnemySpawnCooldownFinished;

    protected override void InvokeFinishEvent()
    {
        EnemySpawnCooldownFinished();
    }
}
