using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    //public float projectileDamage;
    //int enemiesPassedThrough;

    void Start()
    {
        
        //enemiesPassedThrough = 0;
    }

    // Update is called once per frame
    void Update()

    {
        transform.Translate(Vector2.up * Time.deltaTime * projectileSpeed);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Monster"))
        //{
        //    Destroy(gameObject);
        //}

        //if (other.gameObject.CompareTag("Enemy"))
        //{
        //    enemiesPassedThrough++;
        //    if (enemiesPassedThrough > 1)
        //    {
        //        Destroy(gameObject);
        //    }
        //}

    }
}
