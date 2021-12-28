public abstract class BossBunnyBaseState
{
    public abstract void EnterState(BossBunnyStateManager stateManager, float? timeInState = null);
    public abstract void UpdateState(BossBunnyStateManager stateManager);
}
