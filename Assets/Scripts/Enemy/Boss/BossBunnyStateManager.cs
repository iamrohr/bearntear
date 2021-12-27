using System;
using UnityEngine;

public class BossBunnyStateManager : MonoBehaviour
{
    private BossBunnyBaseState currentState;
    public BossBunnyIdleState IdleState = new BossBunnyIdleState();
    public BossBunnyChaseState ChaseState = new BossBunnyChaseState();

    [NonSerialized] public Animator animator;
    [NonSerialized] public BossBunnyMovement movement;
    [NonSerialized] public Vector2 playerPos;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<BossBunnyMovement>();
        playerPos = GameObject.FindGameObjectWithTag("PlayerHolder").transform.position;
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
