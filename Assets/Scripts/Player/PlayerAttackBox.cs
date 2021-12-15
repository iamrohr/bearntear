using System;
using UnityEngine;


public class PlayerAttackBox : MonoBehaviour
{
    [Serializable] private enum AttackType { Swipe, SwipeFinal, Bash, Slam }
    
    public int damage;
    public float timeKnocked = 2f;
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

        if (!other.CompareTag("Enemy"))
            return;

        if (canDamage)
            Attack(other);

        if (attackType == AttackType.Swipe)
            canDamage = false;            
    }

    private void Attack(Collider2D other)
    {
        other.GetComponent<Enemy>().TakeDamage(damage);
        var stateManager = other.GetComponent<EnemyStateManager>();
        stateManager.EnemyKnocked(timeKnocked, timeStunned);
    }
}
