using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed, dashTime, dashCooldown;
    public AudioSource dashSound;
    
    private bool canDash = true;
    private Rigidbody2D rb;
    private PlayerMovement pm;
    private Player player;
    private PlayerInput playerInput;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponentInParent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
    }


    public void Dash()
    {
        float xInput = playerInput.xInput;
        if (!(playerInput.dash && xInput != 0 && canDash)) return;
            
        dashSound.Play();

        canDash = false;
        player.EnterState(PlayerState.Dashing, dashTime);

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
        player.LeaveState(PlayerState.Dashing);
    }

    private void TurnOnCanDash()
    {
        canDash = true;
    }
}
