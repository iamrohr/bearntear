 using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D enemyRB;
    public GameObject player;
    public GameObject cottonDrop;

    public int currentHealth, maxHealth;
    public int dropRange;
    public int giveTear = 20;
    public int pauseTearDecrease = 3;
  
    public Vector2 enemyPos;

    public AudioClip [] enemyDie;
    public float enemyDieVolume = 1f;

    public ParticleSystem psDie;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    private void Update()
    {
        enemyPos = enemyRB.transform.localPosition;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            InstansiateDrop();
            EnemyDieSound();
            psDie.gameObject.SetActive(true);
            psDie.transform.parent = null;
            player.GetComponent<TearBarOnPlayer>().GetTear(giveTear);
            player.GetComponent<TearBarOnPlayer>().TearDecreaseOff(pauseTearDecrease);
            Destroy(this.gameObject);
        }
    }

    private void InstansiateDrop()
    {
        int RandomNumber = Random.Range(1, dropRange + 1);

        if(RandomNumber == dropRange)
        {
            Instantiate(cottonDrop, new Vector2(enemyPos.x, enemyPos.y), Quaternion.identity);           
        }
        
    }

    void EnemyDieSound()
    {
        AudioManager.Instance.sfxAudioSource.PlayOneShot(enemyDie[Random.Range(0, enemyDie.Length)], enemyDieVolume);
    }

}
