using UnityEngine;

public class BossBunnyChargeState : BossBunnyBaseState
{
    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        stateManager.animator.SetTrigger("Charge");
    }

    public override void UpdateState(BossBunnyStateManager stateManager)
    {
        if (!stateManager.attack.isCharging) return;

        stateManager.movement.MoveTowards(stateManager.playerTransform.position, stateManager.attack.chargeSpeed);
    }
}