using UnityEngine;

public class BossBunnyChargeCollider : MonoBehaviour
{
    public int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            CameraShake.Instance.Shake(0.3f, 0.4f);
            GetComponentInParent<BossBunnyAttack>().StopCharge();
        }
    }
}
