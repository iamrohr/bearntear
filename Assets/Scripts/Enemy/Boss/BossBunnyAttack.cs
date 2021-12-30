using UnityEngine;

public class BossBunnyAttack : MonoBehaviour
{
    public float attackRange, aggroRange;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject punchAttack;
    private BossBunnyMovement movement;

    private void Awake()
    {
        movement = GetComponent<BossBunnyMovement>();
    }

    public void Hit()
    {
        movement.TurnToPlayer();
        var attackObject = Instantiate(punchAttack, attackPoint.position, Quaternion.identity);
        attackObject.transform.SetParent(transform);
    }
}
