using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [NonSerialized] public float xInput, yInput;
    [NonSerialized] public bool attackDown, attackHold, attackUp, jump, dash, shoot;
    private bool prolongedAttackUp;

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        attackDown = Input.GetButtonDown("Fire1");
        attackHold = Input.GetButton("Fire1");
        attackUp = Input.GetButtonUp("Fire1");
        jump = Input.GetButtonDown("Jump");
        dash = Input.GetButtonDown("Fire3");
        shoot = Input.GetButton("Shoot");
    }
}
