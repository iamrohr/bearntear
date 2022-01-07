using UnityEngine;

public class EnemyStunState : EnemyBaseState
{
    public override void EnterState(EnemyStateManager enemy)
    {
        int rnd = Random.Range(0, enemy.animationHurt.Length);
        //Debug.Log("I am in stun");
        //enemy.rbHolder.velocity *= 0;
        enemy.TakeDamageSound();
        enemy.animationHurt[rnd].SetActive(true);
        enemy.animator.SetTrigger("Stunned");
    }

    public override void UpdateState(EnemyStateManager enemy)
    {


    }

    public override void OnCollisionEnter2D(EnemyStateManager enemy, Collision2D collision)
    {

    }

    public override void OnTriggerStay2D(EnemyStateManager enemy, Collider2D collision)
    {

    }
}


