using System;
using UnityEngine;

public enum HorFacing {Right, Left};

public class PlayerMovement : MonoBehaviour
{
    public float speed, vertSpeedFactor, turnDelay, speedMulWhenAttacking;
    [NonSerialized] public HorFacing horFacing;
    
    private bool canTurn, immobilized;
    private Rigidbody2D rb;
    private Player player;
    private SpriteRenderer sr;
    private PlayerInput playerInput;
    private Transform _transform;

    private void Awake()
    {
        _transform = GetComponentInParent<Transform>();
        player = GetComponent<Player>();
        rb = GetComponentInParent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        canTurn = true;
        horFacing = HorFacing.Right;
    }

    public void MoveUpdate()
    {
        float x = playerInput.xInput;
        float y = playerInput.yInput;

        if (x == 0 && y == 0 && rb.velocity.magnitude > 0 || immobilized )
        {
            rb.velocity = Vector2.zero;
            return;
        }

        Vector2 newVelocity = new Vector2(x, y);
        newVelocity = newVelocity.normalized * speed;
        newVelocity = new Vector2(newVelocity.x, newVelocity.y * vertSpeedFactor);

        switch (player.state)
        {
            case PlayerState.Idle:
                if (newVelocity.magnitude > 0)
                    player.playerSM.EnterState(PlayerState.Moving);
                break;
            case PlayerState.Moving:
                rb.velocity = newVelocity;
                if (rb.velocity.magnitude <= 0)
                    player.playerSM.LeaveState(PlayerState.Moving);
                break;
            case PlayerState.Attacking:
                rb.velocity = newVelocity * speedMulWhenAttacking;
                break;
            case PlayerState.Jumping:
                rb.velocity = newVelocity;
                break;
            default:
                return;
        }

        if (canTurn && 
            (x > 0 && horFacing == HorFacing.Left 
            || x < 0 && horFacing == HorFacing.Right))
        {
            canTurn = false;
            Invoke(nameof(ChangeHorFacing), turnDelay);
        }
    }

    public void Immobilize(float seconds)
    {
        immobilized = true;
        CancelInvoke(nameof(TurnOffImmobilized));
        Invoke(nameof(TurnOffImmobilized), seconds);
    }

    private void TurnOffImmobilized()
    {
        immobilized = false;
    }

    private void ChangeHorFacing()
    {
        if (horFacing == HorFacing.Left)
        {
            horFacing = HorFacing.Right;
            var x = Mathf.Abs(_transform.localScale.x);
            _transform.localScale = new Vector2(x, _transform.localScale.y);
        }
        else
        {
            horFacing = HorFacing.Left;
            var x = Mathf.Abs(_transform.localScale.x) * -1; 
            _transform.localScale = new Vector2(x , _transform.localScale.y);
        }
        canTurn = true;
    }
}