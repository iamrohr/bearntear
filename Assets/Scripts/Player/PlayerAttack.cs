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
    public float bashRecoveryTime;
    public float comboInterval;
    public float slamTime;
    public float slamHeight;
    public float slamRecoveryTime;
    public int comboTotal;

    [Header("Other")]
    public GameObject swipeAttack;
    public GameObject bashAttack;
    public GameObject slamAttack;
    public AudioSource swingSound;

    [NonSerialized] public int comboCurrent;

    private float attackTimer, prevComboTime;
    private bool canAttack = true, queuedAttack;
    private PlayerMovement playerMovement;
    private Player player;
    private PlayerJump playerJump;
    private Transform _transform;
    private PlayerInput playerInput;
    private Rigidbody2D rb;

    public CameraShake cameraShake; // JAMES KALNINS


    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
        playerJump = GetComponent<PlayerJump>();
        playerInput = GetComponent<PlayerInput>();
        _transform = transform;
    }

    private void Start()
    {
        comboCurrent = 1;
    }

    public void Attack()
    {
        if (playerInput.attackDown && canAttack)
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

        if (playerInput.attackHold && canAttack)
        {
            if (player.state == PlayerState.Attacking)
                attackTimer += Time.deltaTime;
            else
                attackTimer = 0;
        }
        
        if (playerInput.attackUp && player.state == PlayerState.Attacking)
        {
            if (canAttack)
            {
                CancelInvoke(nameof(AttackIfQueued));
                MeleeAttack();
            }
            else
                queuedAttack = true;
            
            attackTimer = 0;
        }
    }

    private void MeleeAttack()
    {

        Vector2 attackPos = new Vector2(_transform.position.x, _transform.position.y);

        if (playerMovement.horFacing == HorFacing.Left)
            attackPos += Vector2.left * attackOffSetX;
        else
            attackPos += Vector2.right * attackOffSetX;

        if (attackTimer < timeForBash)
            SwipeAttack(attackPos);
        else
            BashAttack(attackPos);

        canAttack = false;
    }

    private void SwipeAttack(Vector2 attackPos)
    {
        var attackObject = Instantiate(swipeAttack, attackPos, Quaternion.identity);
        attackObject.transform.SetParent(_transform);

        if (comboCurrent > 1 && comboInterval < Time.time - prevComboTime)
            comboCurrent = 1;

        prevComboTime = Time.time;

        if (comboCurrent >= comboTotal)
        {
            swingSound.pitch = 1;
            player.animator.SetTrigger("Swipe1");

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
        player.animator.SetTrigger("Bash");
        
        swingSound.pitch = 0.85f;
        swingSound.Play();

        comboCurrent = 1;
        AttackCooldown(bashCooldown);

        var attackObject = Instantiate(bashAttack, attackPos, Quaternion.identity);
        attackObject.transform.SetParent(_transform);

        StartCoroutine(cameraShake.Shake(0.3f, 0.2f)); // JAMES KALNINS

        playerMovement.Immobilize(bashRecoveryTime);
    }

    private IEnumerator SlamAttack()
    {
        playerJump.CancelJump();
        rb.velocity = Vector2.zero;
        float groundedY = playerJump.groundedY;
        float t = 0, startY = _transform.localPosition.y;

        while (t < 1)
        {
            float tween = t * t;
            float y = _transform.localPosition.y;

            y = startY + slamHeight * tween;
            _transform.localPosition = new Vector2(_transform.localPosition.x, y);

            t += Time.deltaTime / slamTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);
        t = 0;
        startY = _transform.localPosition.y;
        float distance = startY - groundedY;

        while (t < 1)
        {
            float tween = t * t;
            float y = _transform.localPosition.y;

            y = startY - distance * tween;
            _transform.localPosition = new Vector2(_transform.localPosition.x, y);

            t += Time.deltaTime / slamTime;
            yield return null;
        }

        _transform.localPosition = new Vector2(_transform.localPosition.x, groundedY);
        playerJump.grounded = true;

        StartCoroutine(cameraShake.Shake(0.3f, 0.4f));

        var attackObject = Instantiate(slamAttack, _transform.position, Quaternion.identity);
        attackObject.transform.SetParent(_transform);

        yield return new WaitForSeconds(slamRecoveryTime);
        player.LeaveState(PlayerState.Slamming);

        yield return null;
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
            MeleeAttack();
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
