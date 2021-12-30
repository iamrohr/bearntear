using UnityEngine;

public class BossBunnyAttack : MonoBehaviour
{
    public float attackRange, aggroRange, chargeSpeed, chargeRecoveryTime;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject punchAttack;
    [SerializeField] private GameObject chargeAttack;
    private BossBunnyMovement movement;
    public bool isCharging;

    private void Awake()
    {
        movement = GetComponent<BossBunnyMovement>();
    }

    private void Start()
    {
        isCharging = false;
    }

    public void Hit()
    {
        movement.TurnToPlayer();
        var attackObject = Instantiate(punchAttack, attackPoint.position, Quaternion.identity);
        attackObject.transform.SetParent(transform);
    }

    public void StartCharge()
    {
        chargeAttack.SetActive(true);
        isCharging = true;
    }

    public void StopCharge()
    {
        chargeAttack.SetActive(false);
        isCharging = false;

        var stateManager = GetComponent<BossBunnyStateManager>();
        stateManager.SwitchState(stateManager.IdleState, chargeRecoveryTime);
    }
}
