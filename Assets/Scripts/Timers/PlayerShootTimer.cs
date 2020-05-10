using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootTimer : Timer
{
    public static event Action ShootCooldownFinished;

    protected override void InvokeFinishEvent()
    {
        ShootCooldownFinished();
    }
}
