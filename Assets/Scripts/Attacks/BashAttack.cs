using System;
using UnityEngine;

public class BashAttack : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            //TODO: Damage enemies
        }
    }

}
