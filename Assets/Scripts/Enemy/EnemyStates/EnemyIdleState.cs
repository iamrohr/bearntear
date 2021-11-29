using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{


    public bool attackActive = true;
    private bool canAttackPlayer = true;

    public override void EnterState(EnemyStateManager enemy)
    {
        Debug.Log("Hello, I am the Enter state of the Idle state using the Enemy State Manager");


    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void OnCollisionEnter(EnemyStateManager enemy)
    {

    }
}
