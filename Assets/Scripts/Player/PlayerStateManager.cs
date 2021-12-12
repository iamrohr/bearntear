using System;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerState[] move, attack, jump, dash, shoot;

    private Player player;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private PlayerDash playerDash;
    private PlayerAttack playerAttack;
    private PlayerShoot playerShoot;
    private (PlayerState[], Action)[] stateFunctions;
    

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerDash = GetComponent<PlayerDash>();
        playerAttack = GetComponent<PlayerAttack>();
        playerShoot = GetComponent<PlayerShoot>();
        player = GetComponent<Player>();
    }

    void Start()
    {
        stateFunctions = new (PlayerState[], Action)[]
        {
            (move, ()=> playerMovement.Move()),
            (attack, ()=> playerAttack.Attack()),
            (jump, ()=> playerJump.Jump()),
            (dash, ()=> playerDash.Dash()),
            (shoot, ()=> playerShoot.Shoot())
        };
    }

    void Update()
    {
        for (int i = 0; i < stateFunctions.Length; i++)
        {
            for (int j = 0; j < stateFunctions[i].Item1.Length; j++)
            {
                var stateFunction = stateFunctions[i];
                if (player.state == stateFunction.Item1[j])
                {
                    stateFunction.Item2();
                    break;
                }
            }
        }
    }
}