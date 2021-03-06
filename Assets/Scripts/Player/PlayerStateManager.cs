using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private PlayerState[] moveStates, attackStates, jumpStates, dashStates, shootStates, slamStates;
    private Action move, attack, jump, dash, shoot, slam;
    private Player player;
    private PlayerJump playerJump;
    private PlayerAttack playerAttack;
    private (PlayerState[], Action)[] actionStates;


    private void Awake()
    {
        player = GetComponent<Player>();
        playerJump = GetComponent<PlayerJump>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        move = () => GetComponent<PlayerMovement>().MoveUpdate();
        attack = () => playerAttack.AttackUpdate();
        jump = () => playerJump.JumpUpdate();
        dash = () => GetComponent<PlayerDash>().DashUpdate();
        shoot = () => GetComponent<PlayerShoot>().Shoot();
        slam = () => GetComponent<PlayerSlam>().SlamUpdate();

        actionStates = new (PlayerState[], Action)[]
        {
            (moveStates, move),
            (attackStates, attack),
            (jumpStates, jump),
            (dashStates, dash),
            (shootStates, shoot),
            (slamStates, slam)
        };
    }

    private void Update()
    {
        for (int i = 0; i < actionStates.Length; i++)
        {
            for (int j = 0; j < actionStates[i].Item1.Length; j++)
            {
                var actionState = actionStates[i];
                if (player.state == actionState.Item1[j])
                {
                    actionState.Item2();
                    break;
                }
            }
        }
    }

    public void EnterState(PlayerState newState)
    {
        if (newState == player.state) return;

        player.state = newState;

        switch (player.state)
        {
            case PlayerState.Idle:
                player.animator.SetTrigger("Idle");
                break;
            case PlayerState.Moving:
                player.animator.SetTrigger("Walk");
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.Jumping:
                player.animator.SetTrigger("Jump");
                playerAttack.canAttack = true;
                playerAttack.queuedAttack = false;
                break;
            case PlayerState.Dashing:
                player.animator.SetTrigger("Dash");
                break;
            case PlayerState.Slamming:
                player.animator.SetTrigger("Jump");
                break;
            default:
                goto case PlayerState.Idle;
        }
    }

    public void LeaveState(PlayerState state)
    {
        if (state != player.state) return;

        switch (state)
        {
            //case PlayerState.Idle:
            //    break;
            //case PlayerState.Moving:
            //    break;
            //case PlayerState.Attacking:
            //    break;
            //case PlayerState.Jumping:
            //    break;
            case PlayerState.Dashing:
                if (!playerJump.grounded)
                    EnterState(PlayerState.Jumping);
                else
                    goto default;
                break;
            default:
                EnterState(PlayerState.Idle);
                break;
        }
    }
}