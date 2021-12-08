using UnityEngine;

public enum AttackType {Swipe, Bash }

public class PlayerAttackBox : MonoBehaviour
{
    public int damage;
    public AttackType attackType;
    public float comboMultiplier;
    public bool comboFinal;
    public float stunTime = 1.5f;
    public float knockDistance = 2f;

    void Start()
    {
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

        if (attackType == AttackType.Swipe && comboFinal)
        {
            other.GetComponent<Enemy>().TakeDamage((int)(damage * comboMultiplier));
        }
        else
        {
            Debug.Log("Attack!");
            other.GetComponent<Enemy>().TakeDamage(damage);
            var stateManager = other.GetComponent<EnemyStateManager>();
            stateManager.EnemyStun(stunTime);
            stateManager.EnemyKnocked(knockDistance);
        }
    }

}
