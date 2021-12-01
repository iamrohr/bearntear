using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    public int damage;
    
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().TakeDamage(damage);
    }

}
