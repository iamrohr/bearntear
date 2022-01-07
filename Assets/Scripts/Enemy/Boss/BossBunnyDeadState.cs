public class BossBunnyDeadState : BossBunnyBaseState
{
    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        stateManager.movement.StopMoving();
        stateManager.animator.SetTrigger("Die");
    }
}