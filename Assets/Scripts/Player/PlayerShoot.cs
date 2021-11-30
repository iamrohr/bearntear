using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    //public GameObject player;
    Vector2 lookDirection;
    float lookAngle;
    public float shootCoolDown = 0.4f;

    Player playerHealthTestScript;
    PlayerMovement playerMovementScript;

    public AudioSource shootSound;

    private void Start()
    {
        playerHealthTestScript = GetComponent<Player>();
        playerMovementScript = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        shootCoolDown += Time.deltaTime;
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookDirection.Normalize();
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButtonDown(1) && shootCoolDown > 0.4f)
        {
            // take damage
            playerHealthTestScript.TakeDamage(10);
            Shoot();
        }

    }

    void Shoot()

    {
            shootCoolDown = 0;


        if (playerMovementScript.horFacing == HorFacing.Left)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Vector3 shootDir = new Vector3(-1, 0, 0);
            newProjectile.GetComponent<Projectile>().Setup(shootDir);

            shootSound.Play();
        }

        if (playerMovementScript.horFacing == HorFacing.Right)
        {
            GameObject newProjectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Vector3 shootDir = new Vector3(1, 0, 0);
            newProjectile.GetComponent<Projectile>().Setup(shootDir);

            shootSound.Play();
        }



        //GameObject newProjectile = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        //Vector3 shootDir = new Vector3(lookDirection.x, lookDirection.y, 0);
        //newProjectile.GetComponent<Projectile>().Setup(shootDir);

        //shootSound.Play();
    }
}
