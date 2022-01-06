    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public WaveSpawner spawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("triggered");
            spawner.ableToSpawn = true;
        }
    }
}
