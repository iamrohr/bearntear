using System;
using UnityEngine;

public enum AttackType { Swipe, SwipeFinal, Bash, Slam }

public class PlayerAttackBox : MonoBehaviour
{
    public int damage;
    public float timeKnocked = 0.6f;
    public float timeStunned = 2f;
    [SerializeField] private AttackType attackType;

    private bool canDamage; 

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
                if (canDamage)
                    Attack(other);
                if (attackType == AttackType.Swipe)
                    canDamage = false;
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
        other.GetComponent<Enemy>().TakeDamage(damage);
        var stateManager = other.GetComponent<EnemyStateManager>();
        stateManager.EnemyKnocked(timeKnocked, timeStunned);
    }
}
