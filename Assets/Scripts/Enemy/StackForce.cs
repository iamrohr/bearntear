using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackForce : MonoBehaviour
{
    private Rigidbody2D rb;
    private float timeBeforeForce = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        //Avoid stacking
        if (collision.gameObject.tag == "Enemy")
        {
          EnemyStackPush();
        }
    }

    public bool EnemyStackPush()
    {
        float randomForceDirection = Random.Range(-3, 3);
        timeBeforeForce -= Time.deltaTime;

        if (timeBeforeForce <= 0f)
        {
            Debug.Log("Pushed");
            rb.AddForce(transform.up * randomForceDirection, ForceMode2D.Force);
            rb.AddForce(transform.right * randomForceDirection, ForceMode2D.Force);
            timeBeforeForce = 1f;
            return false;
        }
        return true;

    }
}
