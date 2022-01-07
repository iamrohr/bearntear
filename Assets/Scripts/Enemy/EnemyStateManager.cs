using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyBaseState currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyChaseState ChaseState = new EnemyChaseState();
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyReturnHomeState ReturnHomeState = new EnemyReturnHomeState();
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyWaitState WaitState = new EnemyWaitState();
    public EnemyStunState StunState = new EnemyStunState();
    public EnemyInfiniteChase infiniteChaseState = new EnemyInfiniteChase();

    [Header("Components")]
    [HideInInspector] public GameObject player;
    [HideInInspector] public Rigidbody2D rbHolder;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Enemy enemyScript;
    [HideInInspector] public StackForce stackForceScript;
    public GameObject animationDie;
    public GameObject []animationHurt;

    [Header("Attributes")]
    [HideInInspector] public float forceIdleStateTimer = 2f;
    [HideInInspector] public Vector2 newEnemyPosition;
    [HideInInspector] public Vector2 enemyStartPosition;
    [HideInInspector] public bool arrivedAtRandPos;
    [HideInInspector] public bool moveToStart;
    [HideInInspector] public float agroRangeRand;
    [HideInInspector] public float stackPushCountdown = 1f;
    public float offsetFollowPlayerY = 1.5f;
    public float agroRange = 2f;
    public float agroRandomRange = 5f;
    public float reactionTime = 0.15f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public bool canAttackPlayer = true;
    public float waitBetweenAttack = 0.25f;

    [Header("Sound")]
    public AudioClip[] takeDamageSound;
    public float takeDamageVolume = 1f;

    private Coroutine currentSwitchState;

    void Start()
    {
        //Random movement speed
        float randomSpeed = Random.Range(1f, 1.3f);
        moveSpeed = moveSpeed * randomSpeed;

        player = GameObject.FindGameObjectWithTag("PlayerHolder");

        //enemyTF = transform.parent;
        rbHolder = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyScript = GetComponent<Enemy>();
        stackForceScript = GetComponentInParent<StackForce>();

        //Starting state for the Enemy state machine
        currentState = IdleState;
        currentState.EnterState(this);

        //Get enemy start position
        enemyStartPosition = new Vector2(transform.position.x, transform.position.y);

        agroRangeRand = Random.Range(agroRange, agroRandomRange);
        agroRange = agroRangeRand;

        //Gives a little bit of randomness to the attackrange
        attackRange = Random.Range(attackRange, attackRange + 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        currentState.OnTriggerStay2D(this, collision);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(EnemyBaseState state, float switchTime = 0)
    {
        if (currentSwitchState != null)
        {
            StopCoroutine(currentSwitchState);
        }

        if (switchTime != 0)
        {
            currentSwitchState = StartCoroutine(SwitchStateInTime(state, switchTime));
            currentState = WaitState;
            WaitState.EnterState(this);
        }
        else
        {
            currentState = state;
            state.EnterState(this);
        }
    }

    private IEnumerator SwitchStateInTime(EnemyBaseState state, float switchTime)
    {
        yield return new WaitForSeconds(switchTime);
        currentState = state;
        state.EnterState(this);
    }

    public Vector2 EnemyRandPos(float distance)
    {
        return new Vector2((Random.Range(distance * -1, distance)), Random.Range(distance * -1, distance)) + (Vector2)transform.position;
    }

    public void EnemyKnocked(float timeKnocback = 0, float knockBackPower = 0, float timeStunned = 0)
    {
        rbHolder.velocity *= 0;
        if (transform.position.x < player.transform.position.x)
        {
            rbHolder.AddForce(Vector2.left * 400);
        }
        else
        {
            rbHolder.AddForce(Vector2.right * 400);
        }

        StartCoroutine(EnemyKnockback(timeKnocback, knockBackPower, timeStunned));
    }

    public IEnumerator EnemyKnockback(float timeKnocked, float knockBackPower, float timeStunned)
    {
        SwitchState(StunState);
        yield return new WaitForSeconds(timeKnocked);
        rbHolder.velocity *= 0;
        yield return new WaitForSeconds(timeStunned);
        SwitchState(IdleState);
    }

    private float SmoothStop(float t)
    {
        return 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t);
    }

    public void StopWalking()
    {
        rbHolder.velocity = Vector2.zero;
        animator.SetTrigger("Idle");
        SwitchState(ReturnHomeState, 2);
    }

    public void TakeDamageSound()
    {
        AudioManager.Instance.sfxAudioSource.PlayOneShot(takeDamageSound[Random.Range(0, takeDamageSound.Length)], takeDamageVolume);
    }

}