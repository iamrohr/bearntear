using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMiniBoss : MonoBehaviour
{
    public GameObject ratMiniBoss;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(ratMiniBoss, new Vector3(124.62f, -3.37f, 0f), transform.rotation);
            gameObject.SetActive(false);
        }
    }
}
