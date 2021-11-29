using System;
using UnityEngine;

public enum HorFacing {Right, Left};

public class PlayerMovement : MonoBehaviour
{
    public float horSpeed, vertSpeed, turnDelay;
    public HorFacing horFacing;

    private Rigidbody2D rb;
    private Player player;
    private float moveLimiter = 0.7f;

    void Start()
    {
        player = GetComponent<Player>();
        horFacing = HorFacing.Right;
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * horSpeed;
        float y = Input.GetAxisRaw("Vertical") * vertSpeed;

        Vector2 newVelocity = new Vector2(x, y);
        
        if (x != 0 && y != 0)
        {
            x *= moveLimiter;
            y *= moveLimiter;
        }

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
                break;
            case PlayerState.Jumping:
                break;
            case PlayerState.Dashing:
                break;
            default:
                goto case PlayerState.Idle;
        }

        if (x > 0 && horFacing == HorFacing.Left)
            Invoke(nameof(ChangeHorFacing), turnDelay);
        else if (x < 0 && horFacing == HorFacing.Right)
            Invoke(nameof(ChangeHorFacing), turnDelay);
    }

    private void ChangeHorFacing()
    {
        if (horFacing == HorFacing.Left)
            horFacing = HorFacing.Right;
        else
            horFacing = HorFacing.Left;
    }
}