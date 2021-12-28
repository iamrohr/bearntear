using System;
using UnityEngine;

public class PlayerAttackBox : MonoBehaviour
{
    public int damage, enemiesAffected;
    public float timeKnocked = 0.6f;
    public float knockBackPower = 200f;
    public float timeStunned = 2f;
    [SerializeField] private AttackType attackType;

    private bool canDamage;
    private int enemyHitCount;

    void Start()
    {
        canDamage = true;
        Destroy(gameObject, 0.1f);
        
        //Temporary alpha change
        Color temp = GetComponent<SpriteRenderer>().color;
        temp.a = 0.4f;
        GetComponent<SpriteRenderer>().color = temp;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Enemy":
                if (enemiesAffected > 0)
                {
                    if (enemyHitCount >= enemiesAffected)
                        canDamage = false;
                    
                    enemyHitCount++;
                }

                if (canDamage)
                    Attack(other);  
                break;
            case "Barricade":
                other.GetComponent<Barricade>().TakeDamage(damage, attackType);
                break;
            default:
                break;
        }
    }

    private void Attack(Collider2D other)
    {
        other.GetComponent<IDamageable>().TakeDamage(damage);
        var stateManager = other.GetComponent<EnemyStateManager>();

        if (stateManager != null)
            stateManager.EnemyKnocked(timeKnocked, knockBackPower, timeStunned);
    }
}
