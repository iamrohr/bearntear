using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public int damage;
    
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
    }

}
