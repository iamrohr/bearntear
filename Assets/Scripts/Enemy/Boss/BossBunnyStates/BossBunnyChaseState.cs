using UnityEngine;

public class BossBunnyChaseState : BossBunnyBaseState
{
    private float attackRange;
    private float stateTimer;
    private float? timeInState;

    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        stateTimer = 0;
        this.timeInState = timeInState;
        attackRange = stateManager.attack.attackRange;
        stateManager.animator.SetTrigger("Walk");
        stateManager.bossBunny.aggro = true;
    }

    public override void UpdateState(BossBunnyStateManager stateManager)
    {
        Chase(stateManager);

        if (timeInState != null)
        {
            stateTimer += Time.deltaTime;
            if (stateTimer >= timeInState)
            {
                NextState(stateManager);
            }
        }
    }

    private void Chase(BossBunnyStateManager stateManager)
    {
        stateManager.movement.MoveTowards(stateManager.playerTransform.position);

        var distances = stateManager.movement.AxesDistancesToPlayer();
        if (attackRange > distances.x && (attackRange / 2) > distances.y)
        {
            stateManager.movement.StopMoving();
            stateManager.SwitchState(stateManager.AttackState);
        }
    }

    private void NextState(BossBunnyStateManager stateManager)
    {
        BossBunnyBaseState[] nextAttacks = { stateManager.ShootState/*, stateManager.ChargeState*/ }; //TODO: fix charge
        int randomAttack = Random.Range(0, nextAttacks.Length);

        stateManager.movement.StopMoving();
        stateManager.SwitchState(nextAttacks[randomAttack]);
    }
}
