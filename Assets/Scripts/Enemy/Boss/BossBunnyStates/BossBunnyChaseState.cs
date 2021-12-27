using UnityEngine;

public class BossBunnyChaseState : BossBunnyBaseState
{
    public override void EnterState(BossBunnyStateManager bossBunny)
    {
        if (bossBunny.playerPos == null)
        {
            bossBunny.playerPos = GameObject.FindGameObjectWithTag("PlayerHolder").transform.position;
        }

        bossBunny.animator.SetTrigger("Walk");
    }

    public override void UpdateState(BossBunnyStateManager bossBunny)
    {
        bossBunny.movement.MoveTowards(bossBunny.playerPos);
    }
}
