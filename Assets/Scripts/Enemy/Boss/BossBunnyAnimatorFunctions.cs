using UnityEngine;

public class BossBunnyAnimatorFunctions : MonoBehaviour
{
    private BossBunnyAttack attack;
    private BossBunnyStateManager stateManager;

    private void Awake()
    {
        attack = GetComponentInParent<BossBunnyAttack>();
        stateManager = GetComponentInParent<BossBunnyStateManager>();
    }

    public void Hit()
    {
        attack.Hit();
    }

    public void EnterIdleState(float timeInState)
    {
        stateManager.EnterIdleState(timeInState);
    }

    public void StartCharge()
    {
        attack.StartCharge();
    }

    public void StopCharge()
    {
        attack.StopCharge();
    }
}
