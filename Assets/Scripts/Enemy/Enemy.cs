using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D enemyRB;
    [HideInInspector] public GameObject player;
    [HideInInspector] public Vector2 enemyPos;

    [Header("Attributes")]
    public int currentHealth;
    public int maxHealth;
    public int giveTear = 20;
    public int pauseTearDecrease = 3;

    [Header("Addons")]
    public ParticleSystem particleSystemEnemyDie;
    public GameObject particleGameObj;

    [Header("Drops")]
    public GameObject cottonDrop;
    public int dropRange;
    private float dropOffsetY = 0.75f;

    [Header("Sound")]
    public AudioClip [] enemyDie;
    public float enemyDieVolume = 1f;

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
        Score.instance.AddScore(100);

        if (currentHealth <= 0)
        {
            InstansiateDrop();
            EnemyDieSound();
            particleSystemEnemyDie.transform.parent = null; //Set Particle system parent to null so it does not destroy
            particleSystemEnemyDie.transform.localScale = new Vector3(1, 1, 1);
            particleSystemEnemyDie.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
            particleSystemEnemyDie.gameObject.SetActive(true);
            Destroy(particleGameObj, particleSystemEnemyDie.main.duration); //Deletes the particle system after played
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
            Instantiate(cottonDrop, new Vector2(enemyPos.x, enemyPos.y - dropOffsetY), Quaternion.identity);           
        }  
    }

    void EnemyDieSound()
    {
        AudioManager.Instance.sfxAudioSource.PlayOneShot(enemyDie[Random.Range(0, enemyDie.Length)], enemyDieVolume);
    }

}
