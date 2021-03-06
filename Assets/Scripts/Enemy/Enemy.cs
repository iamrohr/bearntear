using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [HideInInspector] public Rigidbody2D enemyRB;
    [HideInInspector] public Transform enemyTF;
    [HideInInspector] public GameObject player;
    [HideInInspector] public Vector2 enemyPos;
     public bool infiniteChase = false;

    [Header("Attributes")]
    public int currentHealth;
    public int maxHealth;
    public int giveTear = 20;
    public int pauseTearDecrease = 3;

    [Header("Addons")]
    public GameObject animationDie;
    public ParticleSystem particleSystemEnemyDie;
    public GameObject particleGameObj;

    [Header("Drops")]
    public GameObject cottonDrop;
    public int dropRange;
    private float dropOffsetY = 0.75f;

    [Header("Sound")]
    public AudioClip [] enemyDie;
    public float enemyDieVolume = 1f;

    TearMode tearModeScript;

    public GameObject sewingHolder;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        tearModeScript = player.GetComponent<TearMode>();
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

        enemyTF = this.transform.parent;
        enemyRB = enemyTF.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        enemyPos = enemyRB.transform.localPosition;
    }


    public void TakeDamage(int damage)
    {
        infiniteChase = true;
        currentHealth -= damage;
        Score.Instance.AddScore(100);

        if (transform.position.x < player.transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }



        if (currentHealth <= 0)
        {
            if(gameObject.name == "RatMiniBoss")
            {
                Instantiate(sewingHolder, new Vector2(enemyPos.x, enemyPos.y), Quaternion.identity);
            }

            InstansiateDrop();
            EnemyDieSound();
            particleSystemEnemyDie.transform.parent = null; //Set Particle system parent to null so it does not destroy
            particleSystemEnemyDie.gameObject.SetActive(true);
            animationDie.transform.parent = null;
            animationDie.SetActive(true);
            Destroy(particleGameObj, particleSystemEnemyDie.main.duration); //Deletes the particle system after played
            Destroy(animationDie, 1f); //Delete Animation Die 
            if(!tearModeScript.tearModeOn)
            {
                player.GetComponent<TearBarOnPlayer>().GetTear(giveTear);
                player.GetComponent<TearBarOnPlayer>().TearDecreaseOff(pauseTearDecrease);
            }
            
            //Destroy(this.gameObject);
            Destroy(transform.parent.gameObject);
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
