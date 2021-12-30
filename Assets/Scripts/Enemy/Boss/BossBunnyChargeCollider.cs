using UnityEngine;

public class BossBunnyChargeCollider : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            GetComponentInParent<BossBunnyAttack>().StopCharge();
        }
    }
}
