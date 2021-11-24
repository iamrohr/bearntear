using System;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject swipeAttack, bashAttack;
    public float attackOffSetX, swipeCooldown, bashCooldown, timeForBash;

    private float attackTimer = 0;
    private bool canAttack = true;
    private PlayerMovement pm;

    void Start()
    {
        pm = gameObject.GetComponentInParent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetButton("Fire2"))
            attackTimer += Time.deltaTime;
        
        if (Input.GetButtonUp("Fire2"))
        {
            Vector2 attackPos = new Vector2(transform.position.x, transform.position.y);

            if (pm.horDirection == HorDirection.Left)
                attackPos += Vector2.left * attackOffSetX;
            else
                attackPos += Vector2.right * attackOffSetX;
    
            if (attackTimer < timeForBash)
                SwipeAttack(attackPos);
            else
                BashAttack(attackPos);

            attackTimer = 0;
            canAttack = false;
        }
    }

    private void SwipeAttack(Vector2 attackPos)
    {
        //TODO: Trigger animation
        AttackCooldown(swipeCooldown);
        Instantiate(swipeAttack, attackPos, Quaternion.identity);
    }
  
    private void BashAttack(Vector2 attackPos)
    {
        //TODO: Trigger animation
        AttackCooldown(bashCooldown);
        Instantiate(bashAttack, attackPos, Quaternion.identity);
    }

    private void AttackCooldown(float cooldownTime)
    {
        Invoke(nameof(CanAttackToTrue), cooldownTime);
    }

    private void CanAttackToTrue()
    {
        canAttack = true;
    }
}
