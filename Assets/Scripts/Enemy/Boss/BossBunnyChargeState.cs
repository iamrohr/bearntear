using UnityEngine;

public class BossBunnyChargeState : BossBunnyBaseState
{
    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        Debug.Log("Im gonna charge!", stateManager.gameObject);
    }

    public override void UpdateState(BossBunnyStateManager stateManager)
    {

    }
}