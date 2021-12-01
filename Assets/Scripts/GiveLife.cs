using UnityEngine;

public class GiveLife : MonoBehaviour
{
    private int rand;
    public Sprite[] cottonSprites;
    public int giveLife;

    private void Start()
    {
        rand = Random.Range(0, cottonSprites.Length);
        GetComponent<SpriteRenderer>().sprite = cottonSprites[rand];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().GetLife(giveLife);
            Destroy(gameObject);
        }
    }

}
