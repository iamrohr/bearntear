using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    Vector2 lookDirection;
    float lookAngle;
    public float shootCoolDown = 0.4f;

    Player playerScript;
    PlayerMovement playerMovementScript;

    public AudioSource shootSound;

    private void Start()
    {
        playerScript = GetComponent<Player>();
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    public void Shoot()
    {
        shootCoolDown += Time.deltaTime;
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookDirection.Normalize();
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (Input.GetButton("Shoot") && shootCoolDown > 0.4f)
        {
            // take damage
            playerScript.TakeDamage(4);
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
            shootCoolDown = 0;

        if (playerMovementScript.facing == LeftRight.Left)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Vector3 shootDir = new Vector3(-1, 0, 0);
            newProjectile.GetComponent<Projectile>().Setup(shootDir);

            shootSound.Play();
        }

        if (playerMovementScript.facing == LeftRight.Right)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Vector3 shootDir = new Vector3(1, 0, 0);
            newProjectile.GetComponent<Projectile>().Setup(shootDir);

            shootSound.Play();
        }
    }
}
