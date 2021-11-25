using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed, dashTime, dashCooldown;
    
    private bool canDash = true;
    private Rigidbody2D rb;
    private PlayerMovement pm;
    private Vector2 dashDirection;

    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        pm = GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire3") && canDash)
            Dash();
    }

    private void Dash()
    {
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
    }

    private void TurnOnCanDash()
    {
        canDash = true;
    }
}
