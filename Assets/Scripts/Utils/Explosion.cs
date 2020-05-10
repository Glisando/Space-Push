using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Animator anim;

    public void Initialize()
    {
        anim = GetComponent<Animator>();
    }

    public void Play()
    {
        anim.SetBool("Explosion", true);
    }

    public void Stop()
    {
        anim.SetBool("Stop", true);
    }

    public void ReturnToThePool()
    {
        ExplosionPool.ReturnExplosion(gameObject);
    }
}
