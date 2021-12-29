public class BossBunnyChaseState : BossBunnyBaseState
{
    private float attackRange;

    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        attackRange = stateManager.attack.attackRange;
        stateManager.animator.SetTrigger("Walk");
    }

    public override void UpdateState(BossBunnyStateManager stateManager)
    {
        stateManager.movement.MoveTowards(stateManager.playerTransform.position);

        var distances = stateManager.movement.AxesDistancesToPlayer();
        if (attackRange > distances.x && (attackRange / 2) > distances.y)
        {
            stateManager.movement.StopMoving();
            stateManager.SwitchState(stateManager.AttackState);
        }
    }
}
