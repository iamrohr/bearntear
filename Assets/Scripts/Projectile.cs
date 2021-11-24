using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    void Start()
    { 
        
    }

    void Update()

    {
        transform.Translate(Vector2.up * Time.deltaTime * projectileSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // add damage to enemyNPCs
    }
}
