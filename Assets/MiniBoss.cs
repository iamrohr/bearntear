using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    Enemy enemyScript;
    public GameObject sewingHolder;

    void Start()
    {
        enemyScript = this.GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript.currentHealth <= 0)
        {
            Instantiate(sewingHolder, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        }
    }
}
