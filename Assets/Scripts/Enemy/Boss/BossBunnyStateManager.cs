using System;
using UnityEngine;

public class BossBunnyStateManager : MonoBehaviour
{
    public BossBunnyBaseState currentState;
    public BossBunnyIdleState IdleState = new BossBunnyIdleState();
    public BossBunnyChaseState ChaseState = new BossBunnyChaseState();
    public BossBunnyAttackState AttackState = new BossBunnyAttackState();
    public BossBunnyShootState ShootState = new BossBunnyShootState();
    public BossBunnyChargeState ChargeState = new BossBunnyChargeState();
    public BossBunnyHurtState HurtState = new BossBunnyHurtState();

    [NonSerialized] public Animator animator;
    [NonSerialized] public Transform playerTransform;
    [NonSerialized] public BossBunnyMovement movement;
    [NonSerialized] public BossBunnyAttack attack;
    [NonSerialized] public BossBunny bossBunny;

    private void Awake()
    {
        attack = GetComponent<BossBunnyAttack>();
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<BossBunnyMovement>();
        playerTransform = GameObject.FindGameObjectWithTag("PlayerHolder").transform;
        bossBunny = GetComponent<BossBunny>();
    }

    private void Start()
    {
        currentState = IdleState;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void EnterIdleState(float timeInState)
    {
        SwitchState(IdleState, timeInState);
    }

    public void SwitchState(BossBunnyBaseState state, float? timeInState = null)
    {
        currentState = state;
        state.EnterState(this, timeInState);
    }
}
