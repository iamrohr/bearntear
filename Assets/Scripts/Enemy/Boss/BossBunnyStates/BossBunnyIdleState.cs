using UnityEngine;

public class BossBunnyIdleState : BossBunnyBaseState
{
    private float stateTimer;
    private float? timeInState;

    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        stateManager.movement.StopMoving();
        stateManager.animator.SetTrigger("Idle");
        stateTimer = 0;
        this.timeInState = timeInState;
    }

    public override void UpdateState(BossBunnyStateManager stateManager)
    {
        if (timeInState == null)
        {
            NextState(stateManager);
        }
        else
        {
            if (stateTimer < timeInState)
                stateTimer += Time.deltaTime;
            else
                NextState(stateManager);
        }
    }

    private void NextState(BossBunnyStateManager stateManager)
    {
        var distances = stateManager.movement.AxesDistancesToPlayer();
        var attackRange = stateManager.attack.attackRange;
        var aggroRange = stateManager.attack.aggroRange;
        var chaseTimeMin = stateManager.bossBunny.chaseTimeMin;
        var chaseTimeMax = stateManager.bossBunny.chaseTimeMax;
        var timeInNextState = Random.Range(chaseTimeMax, chaseTimeMin);

        switch (distances)
        {
            case var value when value.x <= attackRange && value.y <= attackRange / 2:
                stateManager.SwitchState(stateManager.AttackState);
                break;
            case var value when value.x <= aggroRange && value.y <= aggroRange / 2:
                stateManager.SwitchState(stateManager.ChaseState, timeInNextState);
                break;
            default:
                if (stateManager.bossBunny.aggro)
                    stateManager.SwitchState(stateManager.ChaseState, timeInNextState);
                break;
        }
    }
}
