using UnityEngine;

public class BossBunnyShootState : BossBunnyBaseState
{

    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        Debug.Log("Im gonna shoot!", stateManager.gameObject);
    }

    public override void UpdateState(BossBunnyStateManager bossBunny) 
    {
        
    }
}