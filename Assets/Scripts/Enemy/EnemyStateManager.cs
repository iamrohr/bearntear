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
    //[HideInInspector] public Transform enemyTF;
    [HideInInspector] public Rigidbody2D rbHolder;
    [HideInInspector] public Animator animator;
    [HideInInspector] public Enemy enemyScript;
    public GameObject attackBox;

    [Header("Attributes")]
    [HideInInspector] public float forceIdleStateTimer = 2f;
    [HideInInspector] public Vector2 newEnemyPosition;
    [HideInInspector] public Vector2 enemyStartPosition;
    [HideInInspector] public bool arrivedAtRandPos;
    [HideInInspector] public bool moveToStart;
    [HideInInspector] public float agroRangeRand;
    [HideInInspector] public float offsetFollowPlayerY = 1.5f;

    public float agroRange = 2f;
    public float agroRandomRange = 5f;
    public float reactionTime = 0.15f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public bool canAttackPlayer = true;
    public float waitBetweenAttack = 0.25f;

    private Coroutine currentSwitchState;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerHolder");

        //enemyTF = transform.parent;
        rbHolder = GetComponentInParent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        enemyScript = GetComponent<Enemy>();

        //Starting state for the Enemy state machine
        currentState = IdleState;
        currentState.EnterState(this);

        //Get enemy start position
        enemyStartPosition = new Vector2(transform.position.x, transform.position.y);
        attackBox.SetActive(false);

        agroRangeRand = Random.Range(agroRange, agroRandomRange);
        agroRange = agroRangeRand;

        //Gives a little bit of randomness to the attackrange
        attackRange = Random.Range(attackRange, attackRange + 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    // Update is called once per frame
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
        //Debug.Log("State change");
        yield return new WaitForSeconds(switchTime);
        currentState = state;
        state.EnterState(this);
    }

    //public IEnumerator AttackPlayer()
    //{
    //    yield return new WaitForSeconds(waitBetweenAttack);
    //    attackBox.SetActive(true);
    //    yield return new WaitForSeconds(waitBetweenAttack);
    //    attackBox.SetActive(false);

    //    yield return new WaitForSeconds(waitBetweenAttack);
    //    canAttackPlayer = true;
    //}

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
        Debug.Log("time Knocked: " + timeKnocked + "time Stunned  " + timeStunned);
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

}

//Set Random Reaction time
//public float reactionTime;
//public float reactionTimeRand;
//reactionTimeRand = Random.Range(0.1f, 2f);
//reactionTime = reactionTimeRand;

//Set Random Agro Range

//public bool ReactionTime()
//{
//    reactionTime -= Time.deltaTime;
//    Debug.Log("Reaction time is " + reactionTime);
//    if (reactionTime <= 0f)
//    {
//        reactionTime = reactionTimeRand;
//        return true;
//    }
//    return false;
//}

//public bool WaitBeforeAttack(float wait)
//{
//    wait -= Time.deltaTime;
//    Debug.Log("Reaction time is " + wait);
//    if (wait <= 0f)
//    {
//        return true;
//    }
//    return false;
//}