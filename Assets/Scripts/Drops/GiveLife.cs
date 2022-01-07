using UnityEngine;

public class GiveLife : MonoBehaviour
{
    public Sprite[] cottonSprites;
    private int rand;
    public int giveLife;

    [SerializeField] private AudioClip cottonPickupSound;
    [SerializeField] private float cottonPickupSoundVolume= 1f;
    public GameObject animationPickup;

    public GameObject audioManager;
    public AudioManager audioManagerScript;

    private void Start()
    {
        rand = Random.Range(0, cottonSprites.Length);
        GetComponent<SpriteRenderer>().sprite = cottonSprites[rand];
    }

    public void PickUp(Player player)
    {
        if(cottonPickupSound) 
            AudioManager.Instance.sfxAudioSource.PlayOneShot(cottonPickupSound, cottonPickupSoundVolume);

        Instantiate(animationPickup, new Vector2(transform.position.x, transform.position.y + 1f), Quaternion.identity);
        player.GetLife(giveLife);
        Destroy(gameObject);
    }
}

