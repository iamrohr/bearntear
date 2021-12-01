using UnityEngine;

public class GiveLife : MonoBehaviour
{
    public int giveLife;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().GetLife(giveLife);
            Destroy(gameObject);
        }
    }

}
