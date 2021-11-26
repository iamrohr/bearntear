using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed, dashTime, dashCooldown;
    
    private bool canDash = true;
    private Rigidbody2D rb;
    private PlayerMovement pm;
    private Player player;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        pm = GetComponentInParent<PlayerMovement>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3") && canDash)
            Dash();
    }

    private void Dash()
    {
        player.invulnerable = true;
        canDash = false;
        pm.isDashing = true;
        if (pm.horDirection == HorDirection.Right)
            dashDirection = Vector2.right;
        else
            dashDirection = Vector2.left;

        rb.velocity = dashDirection * dashSpeed;

        Invoke(nameof(CancelDash), dashTime);
        Invoke(nameof(TurnOnCanDash), dashCooldown);
    }

    private void CancelDash()
    {
        rb.velocity = Vector2.zero;
        pm.isDashing = false;
        player.invulnerable = false;
    }

    private void TurnOnCanDash()
    {
        canDash = true;
    }
}
