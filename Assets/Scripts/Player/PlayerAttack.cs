using System;
using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackOffSetX, timeForBash, bashRecoveryTime, comboInterval;
    public float swipeCooldown, bashCooldown, swipeFinalCooldown;
    public int comboTotal;

    public GameObject swipeAttack, bashAttack;
    public AudioSource swingSound;

    [NonSerialized] public int comboCurrent;

    private float attackTimer, prevComboTime;
    public bool canAttack = true, queuedAttack;
    private PlayerMovement playerMovement;
    private Player player;
    private PlayerJump playerJump;
    private Transform _transform;
    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private CameraShake cameraShake;

    private void Awake()
    {
        cameraShake = GameObject.Find("CameraHolder").GetComponent<CameraShake>();
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

    public void AttackUpdate()
    {
        if (playerInput.attackDown && canAttack)
        {
            //CancelInvoke(nameof(CanAttackToTrue));
            player.playerSM.EnterState(PlayerState.Attacking);
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
        if (!playerJump.grounded) return;

        Vector2 attackPos = new Vector2(_transform.position.x, _transform.position.y);

        if (playerMovement.horFacing == HorFacing.Left)
            attackPos += Vector2.left * attackOffSetX;
        else
            attackPos += Vector2.right * attackOffSetX;

        if (player.stage > 0 && attackTimer >= timeForBash)
            BashAttack(attackPos);
        else
            SwipeAttack(attackPos);

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
            AttackCooldown(swipeFinalCooldown);
        }
        else if (comboCurrent % 2 == 0)
        {
            swingSound.pitch = 1.2f;
            player.animator.SetTrigger("Swipe1");

            comboCurrent++;
            AttackCooldown(swipeCooldown);
        }
        else
        {
            swingSound.pitch = 1.1f;
            player.animator.SetTrigger("Swipe2");

            attackObject.GetComponent<SpriteRenderer>().color = Color.blue; //Temp check
            comboCurrent++;
            AttackCooldown(swipeCooldown);
        }

        swingSound.Play();
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

        StartCoroutine(cameraShake.Shake(0.3f, 0.2f));

        playerMovement.Immobilize(bashRecoveryTime);
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
            MeleeAttack();
            queuedAttack = false;
        }
    }

    private void CanAttackToTrue()
    {
        if (!queuedAttack)
        {
            if (!Input.GetButton("Fire1"))
                player.playerSM.LeaveState(PlayerState.Attacking);

            canAttack = true;
        }
    }
}
