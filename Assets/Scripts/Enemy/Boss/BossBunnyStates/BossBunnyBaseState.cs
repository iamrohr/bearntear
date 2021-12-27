using UnityEngine;

public abstract class BossBunnyBaseState
{
    public abstract void EnterState(BossBunnyStateManager bossBunny);
    public abstract void UpdateState(BossBunnyStateManager bossBunny);
}
