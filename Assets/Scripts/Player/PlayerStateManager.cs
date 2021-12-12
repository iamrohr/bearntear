using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private PlayerState[] moveStates, attackStates, jumpStates, dashStates, shootStates;
    private Action move, attack, jump, dash, shoot;
    private Player player;
    private (PlayerState[], Action)[] actionStates;


    void Awake()
    {
        move = () => GetComponent<PlayerMovement>().Move();
        attack = () => GetComponent<PlayerAttack>().Attack();
        jump = () => GetComponent<PlayerJump>().Jump();
        dash = () => GetComponent<PlayerDash>().Dash();
        shoot = () => GetComponent<PlayerShoot>().Shoot();
        player = GetComponent<Player>();
    }

    void Start()
    {
        actionStates = new (PlayerState[], Action)[]
        {
            (moveStates, move),
            (attackStates, attack),
            (jumpStates, jump),
            (dashStates, dash),
            (shootStates, shoot)
        };
    }

    void Update()
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
}