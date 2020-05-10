using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] int speed = 100;
    [SerializeField] float cooldownDuration = 0.1f;

    Rigidbody2D rb2d;
    new Transform transform;

    float offset = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();

        rb2d.gravityScale = 0;

        StartMoving();
    }

    void StartMoving()
    {
        rb2d.AddForce(Vector2.left * speed);
    }

    private void Update()
    {
        CheckEdges();
    }

    void CheckEdges()
    {
        if (transform.position.x < ScreenDimensions.ScreenLeft - offset ||
            transform.position.x > ScreenDimensions.ScreenRight + offset ||
            transform.position.y > ScreenDimensions.ScreenTop + offset ||
            transform.position.y < ScreenDimensions.ScreenBottom - offset)
        {
            SelfDestroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EnemyAI>(out EnemyAI enemy))
        {
            GameObject explosion = ExplosionPool.GetExplosion();

            explosion.transform.position = transform.position;
            explosion.SetActive(true);
            explosion.GetComponent<Explosion>().Play();

            Destroy(gameObject);
        }
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
