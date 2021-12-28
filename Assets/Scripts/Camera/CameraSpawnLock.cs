using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawnLock : MonoBehaviour
{
    public GameObject spawner;
    public GameObject spawnPoint1LeftCollider;
    public GameObject spawnPoint1RightCollider;

    MoveCamera moveCameraScript;
    WaveSpawner waveSpawnerScript;

    private void Start()
    {
        waveSpawnerScript = spawner.GetComponent<WaveSpawner>();
        moveCameraScript = this.gameObject.GetComponent<MoveCamera>();
    }

    private void Update()
    {
        if (transform.position.x > 34 && waveSpawnerScript.ableToSpawn)
        {
            moveCameraScript.enabled = false;
            spawnPoint1LeftCollider.SetActive(true);
            spawnPoint1RightCollider.SetActive(true);
            // enable left (x pos 23.5) & right (x pos 45.3) collider from first spawn position
        }

        if(!waveSpawnerScript.ableToSpawn)
        {
            moveCameraScript.enabled = true;
            spawnPoint1LeftCollider.SetActive(false);
            spawnPoint1RightCollider.SetActive(false);
        }
    }


}
