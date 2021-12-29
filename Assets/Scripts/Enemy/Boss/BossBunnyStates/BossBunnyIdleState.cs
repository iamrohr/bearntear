using UnityEngine;

public class BossBunnyIdleState : BossBunnyBaseState
{
    private float waitTimer;
    private float? timeInState;

    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        stateManager.movement.StopMoving();
        this.timeInState = timeInState;
        waitTimer = 0;
        stateManager.animator.SetTrigger("Idle");
    }

    public override void UpdateState(BossBunnyStateManager stateManager)
    {
        if (timeInState == null)
        {
            NextState(stateManager);
            return;
        }
        else
        {
            if (waitTimer < timeInState)
                waitTimer += Time.deltaTime;
            else
                NextState(stateManager);
        }
    }

    private void NextState(BossBunnyStateManager stateManager)
    {
        var distances = stateManager.movement.AxesDistancesToPlayer();
        var attackRange = stateManager.attack.attackRange;

        switch (distances)
        {
            case var value when value.x <= attackRange && value.y <= attackRange / 2:
                stateManager.SwitchState(stateManager.AttackState);
                break;
            case var value when value.x > attackRange || value.y > attackRange / 2:
                stateManager.SwitchState(stateManager.ChaseState);
                break;
            default:
                break;
        }
    }
}
