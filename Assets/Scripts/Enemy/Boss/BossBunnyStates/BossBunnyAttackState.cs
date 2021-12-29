public class BossBunnyAttackState : BossBunnyBaseState
{
    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        stateManager.animator.SetTrigger("Hit");
    }

    public override void UpdateState(BossBunnyStateManager bossBunny)
    {

    }
}
