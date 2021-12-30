public abstract class BossBunnyBaseState
{
    public virtual void EnterState(BossBunnyStateManager stateManager, float? timeInState = null) { }
    public virtual void UpdateState(BossBunnyStateManager stateManager) { }
}
