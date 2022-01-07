using UnityEngine;

public class BossBunnyHurtState : BossBunnyBaseState
{
    public override void EnterState(BossBunnyStateManager stateManager, float? timeInState = null)
    {
        stateManager.movement.StopMoving();
        stateManager.animator.SetTrigger("Hurt");

        int rnd = Random.Range(0, stateManager.bossBunny.animationHurt.Length);
        stateManager.bossBunny.animationHurt[rnd].SetActive(true);
    }
}