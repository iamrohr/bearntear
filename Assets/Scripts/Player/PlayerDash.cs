using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed, dashTime, dashCooldown;
    
    private bool canDash = true;
    private Rigidbody2D rb;
    private PlayerMovement pm;
    private Player player;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        float xInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Fire3") && xInput != 0 && canDash)
            Dash(xInput);
    }

    private void Dash(float xInput)
    {
        player.invulnerable = true;
        canDash = false;
        player.state = PlayerState.Dashing;

        Vector2 dashDirection;
        if (xInput > 0)
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
        player.state = PlayerState.Idle;
        player.invulnerable = false;
    }

    private void TurnOnCanDash()
    {
        canDash = true;
    }
}
