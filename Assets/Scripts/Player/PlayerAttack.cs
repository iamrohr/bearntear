using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float swipeCooldown, bashCooldown, swipeFinalCooldown;
    public bool canAttack, queuedAttack;
    [SerializeField] private float attackOffSetX, timeForBash, bashRecoveryTime, comboInterval;
    [SerializeField] private int comboTotal;
    [SerializeField] private GameObject swipeAttack, bashAttack, swipeFinalAttack;
    [SerializeField] private AudioSource swingSound;

    private Vector2 attackPos;
    private int comboCurrent;
    private float attackTimer, prevComboTime, cooldown;
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
        canAttack = true;
        cooldown = 0;
        comboCurrent = 1;
    }

    private void Update()
    {
        if (cooldown > 0)
            cooldown -= Time.deltaTime;
        else if (!canAttack)
            canAttack = true;
    }

    public void AttackUpdate()
    {
        if (queuedAttack && canAttack)
            AttackIfQueued();

        if (playerInput.attackDown && canAttack)
            player.playerSM.EnterState(PlayerState.Attacking);

        if (playerInput.attackHold && canAttack)
        {
            if (player.state == PlayerState.Attacking)
                attackTimer += Time.deltaTime;
            else
                attackTimer = 0;
        }

        if (playerInput.attackUp)
        {
            if (canAttack)
                MeleeAttack();
            else if (cooldown < 0.3f)
                queuedAttack = true;

            attackTimer = 0;
        }
    }

    private void MeleeAttack()
    {
        CancelInvoke(nameof(LeaveAttacking));

        if (!playerJump.grounded) return;

        attackPos = new Vector2(_transform.position.x, _transform.position.y);

        if (playerMovement.facing == LeftRight.Left)
            attackPos += Vector2.left * attackOffSetX;
        else
            attackPos += Vector2.right * attackOffSetX;

        if (player.stage > 0 && attackTimer >= timeForBash)
            player.animator.SetTrigger("Bash");
        else
            SwipeAttack();

        canAttack = false;
    }

    private void SwipeAttack()
    {
        if (comboCurrent > 1 && comboInterval < Time.time - prevComboTime)
            comboCurrent = 1;

        prevComboTime = Time.time;

        if (comboCurrent >= comboTotal)
        {
            var attackObject = Instantiate(swipeFinalAttack, attackPos, Quaternion.identity);
            attackObject.transform.SetParent(_transform);

            swingSound.pitch = 1;
            player.animator.SetTrigger("Swipe1");

            comboCurrent = 1;
            cooldown = swipeFinalCooldown;
            Invoke(nameof(LeaveAttacking), swipeFinalCooldown);
        }
        else
        {
            var attackObject = Instantiate(swipeAttack, attackPos, Quaternion.identity);
            attackObject.transform.SetParent(_transform);
            comboCurrent++;

            if (comboCurrent % 2 == 0)
            {
                swingSound.pitch = 1.2f;
                player.animator.SetTrigger("Swipe1");
            }
            else
            {
                swingSound.pitch = 1.1f;
                player.animator.SetTrigger("Swipe2");
                attackObject.GetComponent<SpriteRenderer>().color = Color.blue; //Temp check
            }

            cooldown = swipeCooldown;
            Invoke(nameof(LeaveAttacking), swipeCooldown);
        }

        swingSound.Play();
    }

    public void BashAttack()
    {
        attackPos = new Vector2(_transform.position.x, _transform.position.y);

        if (playerMovement.facing == LeftRight.Left)
            attackPos += Vector2.left * attackOffSetX;
        else
            attackPos += Vector2.right * attackOffSetX;

        swingSound.pitch = 0.85f;
        swingSound.Play();

        comboCurrent = 1;
        cooldown = bashCooldown;

        var attackObject = Instantiate(bashAttack, attackPos, Quaternion.identity);
        attackObject.transform.SetParent(_transform);

        StartCoroutine(cameraShake.Shake(0.3f, 0.2f));

        playerMovement.Immobilize(bashRecoveryTime);

        Invoke(nameof(LeaveAttacking), bashCooldown);
    }

    private void AttackIfQueued()
    {
        if (queuedAttack)
        {
            MeleeAttack();
            queuedAttack = false;

            if (player.state != PlayerState.Attacking)
                player.playerSM.EnterState(PlayerState.Attacking);
        }
    }
 
    private void LeaveAttacking()
    {
        if (!queuedAttack)
            player.playerSM.LeaveState(PlayerState.Attacking);
    }
}
