using UnityEngine;

public class EnemyGiveDamage : MonoBehaviour
{
    public int giveDamage; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(giveDamage);
        }
    }
}
