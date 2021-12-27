using UnityEngine;

public class BossBunnyStateManager : MonoBehaviour
{
    BossBunnyBaseState currentState;
    public BossBunnyIdleState IdleState = new BossBunnyIdleState();

    private void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BossBunnyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
