using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject swipeAttack, bashAttack;
    public float attackOffSetX, swipeCooldown, bashCooldown, timeForBash, comboInterval;
    public int comboTotal, comboCurrent;

    private float attackTimer, prevComboTime;
    public bool canAttack = true, queuedAttack;
    private PlayerMovement pm;
    private Player player;

    void Start()
    {
        comboCurrent = 1;
        pm = gameObject.GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        if (player.state == PlayerState.Dashing)
            return;

        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            CancelInvoke(nameof(CanAttackToTrue));
        }

        if (Input.GetButton("Fire1") && canAttack)
        {
            attackTimer += Time.deltaTime;
            if (player.state != PlayerState.Attacking)
                player.state = PlayerState.Attacking;
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            if (canAttack)
            {
                if (player.state == PlayerState.Attacking)
                {
                    CancelInvoke(nameof(AttackIfQueued));
                    Attack();
                }
            }
            else
                queuedAttack = true;
        }
    }

    private void Attack()
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

    private void SwipeAttack(Vector2 attackPos)
    {
        var attackObject = Instantiate(swipeAttack, attackPos, Quaternion.identity);
        attackObject.transform.SetParent(gameObject.transform);

        if (comboCurrent > 1 && comboInterval < Time.time - prevComboTime)
            comboCurrent = 1;

        prevComboTime = Time.time;

        if (comboCurrent >= comboTotal)
        {
            //TODO: Trigger Swipe Animation final
            attackObject.GetComponent<PlayerAttackBox>().comboFinal = true;
            attackObject.GetComponent<SpriteRenderer>().color = Color.cyan; //Temp check
            comboCurrent = 1;
        }
        else if (comboCurrent % 2 == 0)
        {
            //TODO: Trigger Swipe Animation A
            comboCurrent++;
        }
        else
        {
            //TODO: Trigger Swipe Animation B
            attackObject.GetComponent<SpriteRenderer>().color = Color.blue; //Temp check
            comboCurrent++;
        }
        AttackCooldown(swipeCooldown);
    }
  
    private void BashAttack(Vector2 attackPos)
    {
        comboCurrent = 1;
        AttackCooldown(bashCooldown);
        var attackObject = Instantiate(bashAttack, attackPos, Quaternion.identity);
        attackObject.transform.SetParent(gameObject.transform);
        //TODO: Trigger animation
    }

    private void AttackCooldown(float cooldownTime)
    {
        Invoke(nameof(CanAttackToTrue), cooldownTime);
        Invoke(nameof(AttackIfQueued), cooldownTime);
    }

    private void AttackIfQueued()
    {
        if (queuedAttack)
        {
            Attack();
            queuedAttack = false;
        }
    }

    private void CanAttackToTrue()
    {
        if (!queuedAttack)
        {
            player.state = PlayerState.Idle;
            canAttack = true;
        }
    }
}
