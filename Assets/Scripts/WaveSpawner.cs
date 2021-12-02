using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {spawning, waiting, counting};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    private float searchCountdown = 1f;

    private SpawnState state = SpawnState.counting;

    private void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    private void Update()
    {

        if (state == SpawnState.waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();
                return;
            }
            else
            {
                return;
            }
        }

        if (waveCountDown <=0)
        {
            if (state != SpawnState.spawning)
            {
                StartCoroutine(SpawnWave (waves[nextWave]) );
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }

    }

    void WaveCompleted()
    {
        Debug.Log("Wave completed)");

        state = SpawnState.counting;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete! Looping");

            //Could add on stat multiplier here or something else that makes it harder.
        }

        nextWave++;
    }


    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if(searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy") == null)
            {
                return false;
            }

        }
        return true;
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        Debug.Log("Spawning wave: " + _wave.name);
        state = SpawnState.spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate); //Could add wave delay here
        }

        state = SpawnState.waiting;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        Debug.Log("Spawning enemy" + _enemy.name);
        Instantiate(_enemy, transform.position, transform.rotation);
    }

}
