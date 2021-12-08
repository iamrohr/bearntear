using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    public EnemyBaseState currentState;
    public EnemyIdleState IdleState = new EnemyIdleState();
    public EnemyChaseState ChaseState = new  EnemyChaseState();
    public EnemyAttackState AttackState = new EnemyAttackState();
    public EnemyReturnHomeState ReturnHomeState = new EnemyReturnHomeState();
    public EnemyPatrolState PatrolState = new EnemyPatrolState();
    public EnemyWaitState WaitState = new EnemyWaitState();
    public EnemyStunState StunState = new EnemyStunState();

    [Header("Components")]
    [HideInInspector] public GameObject player;
    public GameObject attackBox;
    public Rigidbody2D rb;

    [Header("Attributes")]
    [HideInInspector] public Vector2 newEnemyPosition;
    [HideInInspector] public Vector2 enemyStartPosition;
    [HideInInspector] public bool arrivedAtRandPos;
    [HideInInspector] public bool moveToStart;

    public float agroRangeRand;
    public float agroRange = 5f;
    public float agroRangeMultiplier = 5f;

    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public bool canAttackPlayer = true;
    public float waitBetweenAttack = 0.25f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();

        //Starting state for the Enemy state machine
        currentState = IdleState;
        currentState.EnterState(this);

        //Get enemy start position
        enemyStartPosition = new Vector2(transform.position.x, transform.position.y);
        attackBox.SetActive(false);

        agroRangeRand = Random.Range(agroRange, agroRange + agroRangeMultiplier);
        agroRange = agroRangeRand;
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
        StartCoroutine(SwitchStateInTime(state, switchTime));
    }

    private IEnumerator SwitchStateInTime(EnemyBaseState state, float switchTime)
    {
        currentState = WaitState;
        WaitState.EnterState(this);

        yield return new WaitForSeconds(switchTime);
        
        currentState = state;
        state.EnterState(this);
    }

    public IEnumerator AttackPlayer()
    {
        yield return new WaitForSeconds(waitBetweenAttack);
        attackBox.SetActive(true);
        yield return new WaitForSeconds(waitBetweenAttack);
        attackBox.SetActive(false);

        yield return new WaitForSeconds(waitBetweenAttack);
        canAttackPlayer = true;
    }

    public Vector2 EnemyRandPos(float distance)
    {
        return new Vector2((Random.Range(distance * -1, distance)),  Random.Range(distance * -1, distance)) + (Vector2)transform.position;
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