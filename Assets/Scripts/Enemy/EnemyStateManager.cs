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

    [Header("Components")]
    public GameObject player;
    public GameObject attackBox;

    [Header("Attributes")]
    public float agroRangeRand;
    public float agroRange = 5f;
    public float agroRangeMultiplier = 5f;

    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public float returnSpeed = 5f;
    public float reactionTime;
    public float reactionTimeRand;
    public float enemyWaitTime = 2f;
    public bool canAttackPlayer = true;

    public Vector2 newEnemyPosition;

    //Fetch Attributes
    public Vector2 enemyStartPosition;

    public bool arrivedAtRandPos;
    public bool moveToStart;

    void Start()
    {
        //Find the player
        player = GameObject.FindGameObjectWithTag("Player");
        //Starting state for the Enemy state machine
        currentState = IdleState;
        currentState.EnterState(this);

        //Get enemy start position
        enemyStartPosition = new Vector2(transform.position.x, transform.position.y);
        attackBox.SetActive(false);

        //Set Random Reaction time
        reactionTimeRand = Random.Range(0.1f, 2f);
        reactionTime = reactionTimeRand;

        //Set Random Agro Range
        agroRangeRand = Random.Range(agroRange, agroRange + agroRangeMultiplier);
        agroRange = agroRangeRand;

        //Set first move to true (Idle move)
        
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
        yield return new WaitForSeconds(switchTime);
        currentState = state;
        state.EnterState(this);
    }

    //State Machines can only be in one state at a time.

    public IEnumerator AttackPlayer()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        attackBox.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        canAttackPlayer = true;
    }

    public bool ReactionTime()
    {
        reactionTime -= Time.deltaTime;
        Debug.Log("Reaction time is " + reactionTime);
        if (reactionTime <= 0f)
        {
            reactionTime = reactionTimeRand;
            return true;
        }
        return false;
    }

    public Vector2 EnemyRandPos(float distance)
    {
        return new Vector2((Random.Range(distance * -1, distance)),  Random.Range(distance * -1, distance)) + (Vector2)transform.position;
    }
}
