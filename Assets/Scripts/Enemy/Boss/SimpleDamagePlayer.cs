using UnityEngine;

public class SimpleDamagePlayer : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private AudioClip hitSound;

    private void Start()
    {
        Destroy(gameObject, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<IDamageable>().TakeDamage(damage);
            if (hitSound != null)
                AudioManager.Instance.sfxAudioSource.PlayOneShot(hitSound);
        }
    }
}
