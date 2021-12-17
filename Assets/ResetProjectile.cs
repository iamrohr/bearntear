using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProjectile : MonoBehaviour
{
    public GameObject projectile;

    void Start()
    {
        projectile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
