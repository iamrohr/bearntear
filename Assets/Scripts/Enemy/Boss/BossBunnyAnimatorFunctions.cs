using UnityEngine;

public class BossBunnyAnimatorFunctions : MonoBehaviour
{
    private BossBunnyAttack attack;
    private BossBunnyStateManager stateManager;
    [SerializeField] private Animator healthAnimator;

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

    public void TriggerSpawners()
    {
        var spawners = GameObject.FindGameObjectsWithTag("EnemySpawner");

        foreach (var spawner in spawners)
        {
            spawner.GetComponent<WaveSpawner>().ableToSpawn = true;
        }
    }

    public void FadeOutHealthBar()
    {
        healthAnimator.SetTrigger("FadeOut");
    }
}
