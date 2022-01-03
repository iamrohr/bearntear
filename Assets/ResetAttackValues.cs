using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttackValues : MonoBehaviour
{
    public GameObject swipeAttack;
    public GameObject bashAttack;
    public GameObject slamAttack;
    public GameObject projectile;



    void Start()
    {
        swipeAttack.transform.localScale = new Vector3(2, 0.5f, 1);
        bashAttack.transform.localScale = new Vector3(2, 0.4f, 1);
        slamAttack.transform.localScale = new Vector3(5, 0.8f, 1);
        projectile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        swipeAttack.GetComponent<PlayerAttackBox>().damage = 8;
        slamAttack.GetComponent<PlayerAttackBox>().timeKnocked = 0f;
        slamAttack.GetComponent<SpriteRenderer>().enabled = true;
    }
}
