using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    private Player player;
    private PlayerJump playerJump;

    void Start()
    {
        player = GetComponentInParent<Player>();
        playerJump = GetComponentInParent<PlayerJump>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!playerJump.grounded)
            return;

        switch (other.tag)
        {
            case "Cotton":
                other.GetComponent<GiveLife>().PickUp(player);
                break;
            default:
                break;
        }
    }
}
