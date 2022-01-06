using UnityEngine;

public class SimpleDamagePlayer : MonoBehaviour
{
    [SerializeField] private int damage;

    private void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);
        }
    }
}
