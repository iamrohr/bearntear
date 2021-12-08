using UnityEngine;

public class GiveLife : MonoBehaviour
{
    public Sprite[] cottonSprites;
    private int rand;
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

    public void PickUp(Player player)
    {
        if(cottonPickupSound) 
            AudioManager.Instance.sfxAudioSource.PlayOneShot(cottonPickupSound, cottonPickupSoundVolume);

        player.GetLife(giveLife);
        Destroy(gameObject);
    }
}

