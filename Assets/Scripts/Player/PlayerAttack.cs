using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attributes")]
    public float attackOffSetX;
    public float swipeCooldown;
    public float bashCooldown;
    public float timeForBash;
    public float comboInterval;
    public float slamTime;
    public float slamHeight;

    [Header("Other")]
    public GameObject swipeAttack;
    public GameObject bashAttack;
    public GameObject slamAttack;
    public AudioSource swingSound;

    [NonSerialized] public int comboTotal, comboCurrent;

    private float attackTimer, prevComboTime;
    private bool canAttack = true, queuedAttack;
    private PlayerMovement pm;
    private Player player;

    public CameraShake cameraShake; // JAMES KALNINS

    void Start()
    {
        comboCurrent = 1;
        pm = gameObject.GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
    }

    void Update()
    {
        switch (player.state)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Moving:
                break;
            case PlayerState.Attacking:
                break;
            case PlayerState.Jumping:
                break;
            default:
                return;
        }

        if (player.state == PlayerState.Dashing)
            return;

        if (Input.GetButtonDown("Fire1") && canAttack)
        {
            if (player.state == PlayerState.Jumping)
            {
                player.EnterState(PlayerState.Slamming);
                StartCoroutine(SlamAttack());
                return;
            }

            CancelInvoke(nameof(CanAttackToTrue));
            player.EnterState(PlayerState.Attacking);
        }

        if (Input.GetButton("Fire1") && canAttack)
        {
            if (player.state == PlayerState.Attacking)
                attackTimer += Time.deltaTime;
            else
                attackTimer = 0;
        }
        
        if (Input.GetButtonUp("Fire1") && player.state == PlayerState.Attacking)
        {
            if (canAttack)
            {
                CancelInvoke(nameof(AttackIfQueued));
                Attack();
            }
            else
                queuedAttack = true;
            
            attackTimer = 0;
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

        canAttack = false;
    }

    private IEnumerator SlamAttack()
    {
        var playerJump = GetComponent<PlayerJump>();
        playerJump.CancelJump();
        float groundedY = playerJump.groundedY;

        float t = 0, startY = transform.localPosition.y;

        while (t < 1)
        {
            float tween = t * t;
            float y = transform.localPosition.y;

            y = startY + slamHeight * tween;
            transform.localPosition = new Vector2(transform.localPosition.x, y);

            t += Time.deltaTime / slamTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        t = 0;
        startY = transform.localPosition.y;
        float distance = startY - groundedY;

        while (t < 1)
        {
            float tween = t * t;
            float y = transform.localPosition.y;

            y = startY - distance * tween;
            transform.localPosition = new Vector2(transform.localPosition.x, y);

            t += Time.deltaTime / slamTime;
            yield return null;
        }

        transform.localPosition = new Vector2(transform.localPosition.x, groundedY);
        playerJump.grounded = true;
        player.LeaveState(PlayerState.Slamming);

        StartCoroutine(cameraShake.Shake(0.3f, 0.4f));

        var attackObject = Instantiate(slamAttack, transform.position, Quaternion.identity);
        attackObject.transform.SetParent(gameObject.transform);

        yield return null;
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
            swingSound.pitch = 1;
            player.animator.SetTrigger("Swipe1");

            attackObject.GetComponent<PlayerAttackBox>().comboFinal = true;
            attackObject.GetComponent<SpriteRenderer>().color = Color.cyan; //Temp check
            comboCurrent = 1;
        }
        else if (comboCurrent % 2 == 0)
        {
            swingSound.pitch = 1.2f;
            player.animator.SetTrigger("Swipe1");

            comboCurrent++;
        }
        else
        {
            swingSound.pitch = 1.1f;
            player.animator.SetTrigger("Swipe2");

            attackObject.GetComponent<SpriteRenderer>().color = Color.blue; //Temp check
            comboCurrent++;
        }
 
        swingSound.Play();
        AttackCooldown(swipeCooldown);
    }
  
    private void BashAttack(Vector2 attackPos)
    {
        swingSound.pitch = 0.85f;
        swingSound.Play();

        comboCurrent = 1;
        AttackCooldown(bashCooldown);

        var attackObject = Instantiate(bashAttack, attackPos, Quaternion.identity);
        attackObject.transform.SetParent(gameObject.transform);

        StartCoroutine(cameraShake.Shake(0.3f, 0.2f)); // JAMES KALNINS

        player.animator.SetTrigger("Bash");
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
            player.EnterState(PlayerState.Attacking);
            Attack();
            queuedAttack = false;
        }
    }

    private void CanAttackToTrue()
    {
        if (!queuedAttack)
        {
            if (!Input.GetButton("Fire1"))
                player.LeaveState(PlayerState.Attacking);

            canAttack = true;
        }
    }
}
