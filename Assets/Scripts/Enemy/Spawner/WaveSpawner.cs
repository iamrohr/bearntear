using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public enum SpawnState {spawning, waiting, counting};
    [HideInInspector] public bool ableToSpawn; 
        
    //Objects
    public GameObject[] barsList;

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

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    public float waveCountDown;

    public float searchCountdown = 1f;

    public SpawnState state = SpawnState.counting;

    private void Start()
    {
        ableToSpawn = false;

        waveCountDown = timeBetweenWaves;

        if (spawnPoints.Length == 0)
        {
            //Debug.Log("Error, No Spawn Points Available");
        }
    }

    private void Update()
    {
        if (ableToSpawn)
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

            if (waveCountDown <= 0)
            {
                if (state != SpawnState.spawning)
                {
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            }
            else
            {
                waveCountDown -= Time.deltaTime;
            }
        }

    }

    void WaveCompleted()
    {
        state = SpawnState.counting;
        waveCountDown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            //All waves completed
            foreach (GameObject bars in barsList)
            {
                bars.GetComponent<SpriteRenderer>().enabled = true;
            }

            ableToSpawn = false;
            Destroy(transform.parent.gameObject);
        }
        else
        {
        nextWave++;
        }

    }

    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;

        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectsWithTag("Enemy Spawned").Length <= 0)
            {
                return false;
            }
    }
        return true;
    }

    IEnumerator SpawnWave (Wave _wave)
    {
        state = SpawnState.spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(_wave.rate);
        }
        state = SpawnState.waiting;

        yield break;
    }

    void SpawnEnemy (Transform _enemy)
    {
        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);
        _enemy.GetComponentInChildren<Enemy>().infiniteChase = true;
    }

}
