public class BossBunnyHurtState : BossBunnyBaseState
{
    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        stateManager.movement.StopMoving();
        stateManager.animator.SetTrigger("Hurt");
    }

    public override void UpdateState(BossBunnyStateManager stateManager)
    {

    }
}