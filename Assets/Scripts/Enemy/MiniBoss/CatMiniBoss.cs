using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMiniBoss : MonoBehaviour
{
    public GameObject catMiniBoss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            Instantiate(catMiniBoss, new Vector3(125f, -2.63f, 0f), transform.rotation);
            Instantiate(catMiniBoss, new Vector3(130f, -2.63f, 0f), transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
