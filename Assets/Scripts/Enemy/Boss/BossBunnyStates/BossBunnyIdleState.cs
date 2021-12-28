using UnityEngine;

public class BossBunnyIdleState : BossBunnyBaseState
{
    public override void EnterState(BossBunnyStateManager bossBunny)
    {
        bossBunny.animator.SetTrigger("Idle");
    }

    public override void UpdateState(BossBunnyStateManager bossBunny)
    {

    }
}
