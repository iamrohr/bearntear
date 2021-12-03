using UnityEngine;

public class GiveLife : MonoBehaviour
{
    private int rand;
    public Sprite[] cottonSprites;
    public int giveLife;

    [SerializeField] private AudioClip cottonPickupSound;
    [SerializeField] private float cottonPickupSoundVolume= 1f;

    public GameObject audioManager;
    public AudioManager audioManagerScript;

    private void Start()
    {
        rand = Random.Range(0, cottonSprites.Length);
        GetComponent<SpriteRenderer>().sprite = cottonSprites[rand];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           if(cottonPickupSound) AudioManager.Instance.sfxAudioSource.PlayOneShot(cottonPickupSound, cottonPickupSoundVolume);

            other.GetComponent<Player>().GetLife(giveLife);
            Destroy(gameObject);
        }
    }
}

