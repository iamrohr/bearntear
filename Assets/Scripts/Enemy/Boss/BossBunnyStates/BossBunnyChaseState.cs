using UnityEngine;

public class BossBunnyChaseState : BossBunnyBaseState
{
    private float attackRange;

    public override void EnterState(BossBunnyStateManager bossBunny)
    {
        attackRange = bossBunny.attack.attackRange;

        if (bossBunny.playerTransform == null)
        {
            bossBunny.playerTransform = GameObject.FindGameObjectWithTag("PlayerHolder").transform;
        }
        bossBunny.animator.SetTrigger("Walk");
    }

    public override void UpdateState(BossBunnyStateManager bossBunny)
    {
        var distance = bossBunny.playerTransform.position - bossBunny.movement.holderTransform.position;
        bossBunny.movement.MoveTowards(bossBunny.playerTransform.position);

        if (attackRange > distance.x && (attackRange / 2) > distance.y)
        {
            bossBunny.movement.StopMoving();
            bossBunny.SwitchState(bossBunny.IdleState);
        }
    }
}
