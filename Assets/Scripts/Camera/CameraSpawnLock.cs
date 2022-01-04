using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawnLock : MonoBehaviour
{
    public GameObject spawner1;
    public GameObject spawnPoint1LeftCollider;
    public GameObject spawnPoint1RightCollider;

    public GameObject spawner2;
    public GameObject spawnPoint2LeftCollider;
    public GameObject spawnPoint2RightCollider;

    public GameObject backgroundMusic;
    public GameObject enemyWaveMusic;

    MoveCamera moveCameraScript;
    WaveSpawner waveSpawner1Script;
    WaveSpawner waveSpawner2Script;

    private void Start()
    {
        enemyWaveMusic.SetActive(false);
        waveSpawner1Script = spawner1.GetComponent<WaveSpawner>();
        waveSpawner2Script = spawner2.GetComponent<WaveSpawner>();
        moveCameraScript = this.gameObject.GetComponent<MoveCamera>();
    }

    private void Update()
    {
        if (transform.position.x > 34 && transform.position.x < 90 && waveSpawner1Script.ableToSpawn)
        {
            moveCameraScript.enabled = false;
            spawnPoint1LeftCollider.SetActive(true);
            spawnPoint1RightCollider.SetActive(true);
            backgroundMusic.SetActive(false);
            enemyWaveMusic.SetActive(true);
        }

        if(transform.position.x > 34 && transform.position.x < 90 && !waveSpawner1Script.ableToSpawn)
        {
            moveCameraScript.enabled = true;
            spawnPoint1LeftCollider.SetActive(false);
            spawnPoint1RightCollider.SetActive(false);
            backgroundMusic.SetActive(true);
            enemyWaveMusic.SetActive(false);
        }

        if (transform.position.x > 99.5 && waveSpawner2Script.ableToSpawn)
        {
            moveCameraScript.enabled = false;
            spawnPoint2LeftCollider.SetActive(true);
            spawnPoint2RightCollider.SetActive(true);
            backgroundMusic.SetActive(false);
            enemyWaveMusic.SetActive(true);
        }

        if (transform.position.x > 99.5 && !waveSpawner2Script.ableToSpawn)
        {
            moveCameraScript.enabled = true;
            spawnPoint2LeftCollider.SetActive(false);
            spawnPoint2RightCollider.SetActive(false);
            backgroundMusic.SetActive(true);
            enemyWaveMusic.SetActive(false);
        }
    }


}
