using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;
    //public GameObject player;
    public Rigidbody2D rbPlayer;
    Vector2 lookDirection;
    float lookAngle;
    public float shootCoolDown = 0.4f;

    PlayerHealthTEST playerHealthTestScript;

    //public AudioSource shootSound; // put this in soon

    private void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        playerHealthTestScript = GetComponent<PlayerHealthTEST>();
    }

    void Update()
    {
        shootCoolDown += Time.deltaTime;
        lookDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        lookAngle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

        if (Input.GetMouseButtonDown(0) && shootCoolDown > 0.4f)
        {
            // take damage
            playerHealthTestScript.TakeDamage(10);
            Shoot();
        }

        

    }

    void Shoot()

    {
        shootCoolDown = 0;

        GameObject newProjectile = Instantiate(projectilePrefab, new Vector3(rbPlayer.position.x, rbPlayer.position.y, 0), Quaternion.identity);
        newProjectile.transform.up = lookDirection;
                
        //shootSound.Play(); // put this in soon
    }
}
