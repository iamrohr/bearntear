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

    [Header("Components")]
    public Transform player;
    public GameObject attackBox;

    [Header("Attributes")]
    public float agroRange = 8f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public float returnSpeed = 5f;
    public bool canAttackPlayer = true; 

    //Fetch Attributes
    public Vector2 startPosition;

    void Start()
    {
        //Starting state for the Enemy state machine
        currentState = IdleState;
        currentState.EnterState(this);

        //Get enemy start position
        startPosition = new Vector2(transform.position.x, transform.position.y);
        attackBox.SetActive(false);
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

    public void SwitchState(EnemyBaseState state)
    {
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

}