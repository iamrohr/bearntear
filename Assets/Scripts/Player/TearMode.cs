using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TearMode : MonoBehaviour
{
    TearBarOnPlayer tearBarOnPlayerScript;
    PlayerMovement playerMovementScript;
    Camera mainCamera;
    CameraShake cameraShake;

    public GameObject swipeAttack;
    public GameObject bashAttack;
    public GameObject slamAttack;
    public GameObject projectile;

    GameObject tearBarFill;
    GameObject tearModeAnimation;
    AudioSource tearModeSound;
    PlayerAttack playerAttackScript;
    PlayerShoot playerShootScript;

    GameObject lunanewmodel;
    Animator animator;

    public bool tearModeOn = false;

    private void Start()
    {
        mainCamera = Camera.main;
        cameraShake = GameObject.Find("CameraHolder").GetComponent<CameraShake>();
        lunanewmodel = GameObject.Find("lunanewmodel");
        animator = lunanewmodel.GetComponent<Animator>();

        playerShootScript = gameObject.GetComponent<PlayerShoot>();
        playerAttackScript = gameObject.GetComponent<PlayerAttack>();
        playerMovementScript = gameObject.GetComponent<PlayerMovement>();
        tearBarOnPlayerScript = gameObject.GetComponent<TearBarOnPlayer>();
        tearBarFill = GameObject.FindGameObjectWithTag("TearBarFill");
        tearModeAnimation = GameObject.FindGameObjectWithTag("TearModeAnimation");
        tearModeSound = GameObject.FindGameObjectWithTag("TearModeSound").GetComponent<AudioSource>();
    }

    public IEnumerator TearModeStart()
    {
        tearModeOn = true;
        mainCamera.orthographicSize = 4.5f;
        StartCoroutine(cameraShake.ShakeCoroutine(0.5f, 0.4f));
        slamAttack.GetComponent<PlayerAttackBox>().timeKnocked = 4f;
        slamAttack.GetComponent<SpriteRenderer>().enabled = false;
        slamAttack.transform.localScale = new Vector3(7, 7, 1);
        var attackObject = Instantiate(slamAttack, transform.position, Quaternion.identity);
        attackObject.transform.SetParent(transform);
        tearModeAnimation.SetActive(true);

        

        Time.timeScale = 0.001f;

        animator.SetTrigger("TearMode");

        yield return new WaitForSeconds(0.001f);

        mainCamera.orthographicSize = 5.382994f;
        Time.timeScale = 1f;
        playerMovementScript.speed = 12f;
        swipeAttack.GetComponent<PlayerAttackBox>().damage = 20;
        slamAttack.GetComponent<PlayerAttackBox>().timeKnocked = 4f;
        
        playerAttackScript.swipeCooldown = 0.1f;
        playerAttackScript.bashCooldown = 0.1f;
        playerShootScript.shootCoolDown = 0.1f;

        swipeAttack.transform.localScale = new Vector3(3, 0.5f, 1);
        bashAttack.transform.localScale = new Vector3(3, 0.4f, 1);
        slamAttack.transform.localScale = new Vector3(7, 0.8f, 1);
        projectile.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

        StartCoroutine(BlinkingTearBar(20));
    
        yield return new WaitForSeconds(10f);

        tearBarFill.GetComponent<Image>().color = new Color32(100, 196, 255, 255);
        tearModeOn = false;
        playerMovementScript.speed = 6f;
        swipeAttack.GetComponent<PlayerAttackBox>().damage = 8;
        slamAttack.GetComponent<PlayerAttackBox>().timeKnocked = 0f;
        playerAttackScript.swipeCooldown = 0.5f;
        playerAttackScript.bashCooldown = 0.5f;
        playerShootScript.shootCoolDown = 0.4f;

        swipeAttack.transform.localScale = new Vector3(2, 0.5f, 1);
        bashAttack.transform.localScale = new Vector3(2, 0.4f, 1);
        slamAttack.transform.localScale = new Vector3(5, 0.8f, 1);
        projectile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        tearModeAnimation.SetActive(false);

        tearBarOnPlayerScript.currentTear = 0; // to make TearBar 0 after TearMode

        slamAttack.GetComponent<SpriteRenderer>().enabled = true;

    }

    private IEnumerator BlinkingTearBar(int loops)
    {
        for (int i = 0; i < loops; i++)
        {
            tearBarFill.GetComponent<Image>().color = Color.blue;

            yield return new WaitForSeconds(0.25f);

            tearBarFill.GetComponent<Image>().color = Color.red;

            yield return new WaitForSeconds(0.25f);
        }
    }

    private IEnumerator BlinkingTearBarWarning(int loops)
    {
        for (int i = 0; i < loops; i++)
        {
            while(tearBarOnPlayerScript.currentTear == tearBarOnPlayerScript.maxTear)
            {
                tearBarFill.GetComponent<Image>().color = Color.white;

                yield return new WaitForSeconds(0.1f);

                tearBarFill.GetComponent<Image>().color = Color.red;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }


    private void Update()
    {
        if (tearBarOnPlayerScript.currentTear == tearBarOnPlayerScript.maxTear)

        { 

            if(!tearModeOn)
            {
                StartCoroutine(BlinkingTearBarWarning(5));
            }

            if (Input.GetButton("TearMode") && !tearModeOn)
            {
                tearModeSound.Play();
                StartCoroutine(TearModeStart());
                
            }

            if(tearModeOn)
            {
            tearBarOnPlayerScript.currentTear -= 10f * Time.deltaTime; // 100 tearBar points / WaitForSeconds(10) after starting the Coroutine = 10
            }

            // StartCoroutine(BlinkingTearBarWarning(1));
        }
    }
}
