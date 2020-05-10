using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] int speed = 10;
    [SerializeField] float cooldownDuration = 0.1f;
    Rigidbody2D rb2d;
    new Transform transform;

    PlayerShootTimer cooldownTimer;
    Vector2 projectileOffset;
    bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();

        cooldownTimer = gameObject.AddComponent<PlayerShootTimer>();
        cooldownTimer.Duration = cooldownDuration;

        PlayerShootTimer.ShootCooldownFinished += CooldownTimerHandler;

        projectileOffset = new Vector2(1f, -0.025f);
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer.Duration = cooldownDuration;
        float y = CrossPlatformInputManager.GetAxis("Vertical");

        if (y != 0)
            rb2d.velocity = new Vector2(0, y) * speed;
        else
            rb2d.velocity = Vector2.zero;

        if (canShoot &&
            CrossPlatformInputManager.GetAxisRaw("Fire1") != 0)
        {
            cooldownTimer.Run();
            canShoot = false;
            Vector2 projectilePos = transform.position;
            projectilePos += projectileOffset;
            GameObject projectile = PlayerProjectilePool.GetProjectile();
            projectile.transform.position = projectilePos;
            projectile.SetActive(true);
            projectile.GetComponent<PlayerProjectile>().StartMoving();
        }
    }

    void CooldownTimerHandler()
    {
        canShoot = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDisable()
    {
        PlayerShootTimer.ShootCooldownFinished -= CooldownTimerHandler;
    }
}
