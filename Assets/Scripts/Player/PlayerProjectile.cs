using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb2d;
    Animator anim;

    const float ImpulseForce = 200f;

    public void Initialize()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    public void StopMoving()
    {
        rb2d.velocity = Vector2.zero;
    }

    public void StartMoving()
    {
        rb2d.AddForce(Vector2.right * ImpulseForce, ForceMode2D.Force);
    }

    private void OnBecameInvisible()
    {
        PlayerProjectilePool.ReturnProjectile(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("Heart someone");
            PlayerProjectilePool.ReturnProjectile(gameObject);
        }
        StopMoving();
        anim.SetBool("PlayerProjectileExplosion", true);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopMoving();
        anim.SetBool("PlayerProjectileExplosion", true);
    }

    public void ReturnToThePool()
    {
        gameObject.SetActive(false);
    }
}
