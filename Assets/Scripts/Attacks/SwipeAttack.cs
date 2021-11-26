using UnityEngine;

public class SwipeAttack : MonoBehaviour
{
    public int damage = 10;
    
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().TakeDamage(10);
        }
    }

}
