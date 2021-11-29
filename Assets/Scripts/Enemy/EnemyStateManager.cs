using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{

    EnemyBaseState currentState;
    EnemyIdleState IdleState = new EnemyIdleState();
    EnemyTriggerState TriggerState = new  EnemyTriggerState();
    EnemyPunchState PunchState = new EnemyPunchState();

    public Transform player; 

    void Start()
    {
        //Starting state for the Enemy state machine
        currentState = IdleState;
        currentState.EnterState(this);

    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    //State Machines can only be in one state at a time.
}
