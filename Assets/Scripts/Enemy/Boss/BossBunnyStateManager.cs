using System;
using UnityEngine;

public class BossBunnyStateManager : MonoBehaviour
{
    private BossBunnyBaseState currentState;
    public BossBunnyIdleState IdleState = new BossBunnyIdleState();
    public BossBunnyChaseState ChaseState = new BossBunnyChaseState();


    [NonSerialized] public Animator animator;
    [NonSerialized] public Transform playerTransform;
    [NonSerialized] public BossBunnyMovement movement;
    [NonSerialized] public BossBunnyAttack attack;

    private void Awake()
    {
        attack = GetComponent<BossBunnyAttack>();
        animator = GetComponent<Animator>();
        movement = GetComponent<BossBunnyMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("PlayerHolder").transform;
    }

    private void Start()
    {
        currentState = ChaseState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BossBunnyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
