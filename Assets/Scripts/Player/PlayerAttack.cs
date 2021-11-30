using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject swipeAttack, bashAttack;
    public float attackOffSetX, swipeCooldown, bashCooldown, timeForBash;
    public int comboTotal;

    private float attackTimer = 0;
    private bool canAttack = true;
    private PlayerMovement pm;
    private Player player;

    void Start()
    {
        pm = gameObject.GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (player.state == PlayerState.Dashing)
            return;

        if (Input.GetButtonDown("Fire1") && canAttack)
            player.state = PlayerState.Attacking;

        if (Input.GetButton("Fire1") && canAttack)
            attackTimer += Time.deltaTime;
        
        if (Input.GetButtonUp("Fire1") && canAttack)
        {
            Vector2 attackPos = new Vector2(transform.position.x, transform.position.y);

            if (pm.horFacing == HorFacing.Left)
                attackPos += Vector2.left * attackOffSetX;
            else
                attackPos += Vector2.right * attackOffSetX;
    
            if (attackTimer < timeForBash)
                SwipeAttack(attackPos);
            else
                BashAttack(attackPos);

            attackTimer = 0;
            canAttack = false;
        }
    }

    private void SwipeAttack(Vector2 attackPos)
    {
        //TODO: Trigger animation
        AttackCooldown(swipeCooldown);
        Instantiate(swipeAttack, attackPos, Quaternion.identity);
    }
  
    private void BashAttack(Vector2 attackPos)
    {
        //TODO: Trigger animation
        AttackCooldown(bashCooldown);
        Instantiate(bashAttack, attackPos, Quaternion.identity);
    }

    private void AttackCooldown(float cooldownTime)
    {
        Invoke(nameof(CanAttackToTrue), cooldownTime);
    }

    private void CanAttackToTrue()
    {
        player.state = PlayerState.Idle;
        canAttack = true;
    }
}
