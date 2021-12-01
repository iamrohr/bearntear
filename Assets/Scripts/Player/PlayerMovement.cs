using UnityEngine;

public enum HorFacing {Right, Left};

public class PlayerMovement : MonoBehaviour
{
    public float speed, vertSpeedFactor, turnDelay, atkSpeedFactor;
    public HorFacing horFacing;

    private bool canTurn;
    private float diagonalMoveLimiter = 0.7f;
    private Rigidbody2D rb;
    private Player player;
    private SpriteRenderer sr;

    void Start()
    {
        canTurn = true;
        horFacing = HorFacing.Right;
        player = GetComponent<Player>();
        rb = GetComponentInParent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector2 newVelocity = new Vector2(x, y);
        newVelocity = newVelocity.normalized * speed;
        newVelocity = new Vector2(newVelocity.x, newVelocity.y * vertSpeedFactor);

        switch (player.state)
        {
            case PlayerState.Idle:
                if (newVelocity.magnitude > 0)
                    player.state = PlayerState.Moving;
                break;
            case PlayerState.Moving:
                rb.velocity = newVelocity;
                if (rb.velocity.magnitude <= 0)
                    player.state = PlayerState.Idle;
                break;
            case PlayerState.Attacking:
                rb.velocity = newVelocity * atkSpeedFactor;
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Dashing:
                break;
            default:
                goto case PlayerState.Idle;
        }

        if (canTurn && 
            (x > 0 && horFacing == HorFacing.Left 
            || x < 0 && horFacing == HorFacing.Right))
        {
            canTurn = false;
            Invoke(nameof(ChangeHorFacing), turnDelay);
        }
    }

    private void ChangeHorFacing()
    {
        if (horFacing == HorFacing.Left)
        {
            horFacing = HorFacing.Right;
            sr.flipX = false;
        }
        else
        {
            horFacing = HorFacing.Left;
            sr.flipX = true;
        }
        canTurn = true;
    }
}