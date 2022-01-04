using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMiniBoss : MonoBehaviour
{
    public GameObject catMiniBoss;
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(catMiniBoss, new Vector3(125.31f, -2.63f, 0f), transform.rotation);
        }
    }
}
