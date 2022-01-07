using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    private static Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        MovePlayerToSpawnPoint();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            FadeToNextScene();
    }

    private void MovePlayerToSpawnPoint()
    {
        var spawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (spawnPoint == null) return;

        var player = GameObject.FindGameObjectWithTag("PlayerHolder");

        player.transform.position = spawnPoint.transform.position;
    }

    public static void FadeToNextScene()
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete() 
    {
        LoadNextScene();
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
