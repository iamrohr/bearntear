using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TearMode : MonoBehaviour
{
    public TearBarOnPlayer tearBarOnPlayerScript;
    public PlayerMovement playerMovementScript;
    Camera mainCamera;
    CameraShake cameraShake;

    public GameObject swipeAttack;
    public GameObject slamAttack;
    public GameObject tearBarFill;
    public AudioSource tearModeSound;

    public bool tearModeOn = false;
    bool tearBarRed = false;

    private void Start()
    {
        mainCamera = Camera.main;
        cameraShake = GameObject.Find("CameraHolder").GetComponent<CameraShake>();
    }

    public IEnumerator TearModeStart()
    {
        tearModeOn = true;
        mainCamera.orthographicSize = 4.5f;
        StartCoroutine(cameraShake.Shake(0.5f, 0.4f));
        
        Time.timeScale = 0.001f;
        // knockBack effect
        // playSound
        // particleEffect

        yield return new WaitForSeconds(0.001f);

        mainCamera.orthographicSize = 5.382994f;
        Time.timeScale = 1f;
        playerMovementScript.speed = 12f;
        swipeAttack.GetComponent<PlayerAttackBox>().damage = 20;
        //swipeAttack.GetComponent<PlayerAttackBox>().knockDistance = 4f; // enemies die after one swipe anyway
        slamAttack.GetComponent<PlayerAttackBox>().knockDistance = 4f;

        StartCoroutine(BlinkingTearBar(20));
    
        yield return new WaitForSeconds(10f);

        tearBarFill.GetComponent<Image>().color = new Color32(100, 196, 255, 255);
        tearModeOn = false;
        playerMovementScript.speed = 6f;
        swipeAttack.GetComponent<PlayerAttackBox>().damage = 8;
        // swipeAttack.GetComponent<PlayerAttackBox>().knockDistance = 2f; // enemies die after one swipe anyway
        slamAttack.GetComponent<PlayerAttackBox>().knockDistance = 4f;
    }

    private IEnumerator BlinkingTearBar(int loops)
    {
        for (int i = 0; i < loops; i++)
        {
            tearBarFill.GetComponent<Image>().color = Color.blue;

            yield return new WaitForSeconds(0.25f);

            //tearBarOnPlayerScript.currentTear -= 130f * Time.deltaTime;

            tearBarFill.GetComponent<Image>().color = Color.red;

            yield return new WaitForSeconds(0.25f);
        }
    }

    private void Update()
    {
        if (tearBarOnPlayerScript.currentTear == tearBarOnPlayerScript.maxTear)

            if (Input.GetButton("TearMode") && !tearModeOn)
            {
                tearModeSound.Play();
                StartCoroutine(TearModeStart());
                
            }

        if(tearModeOn)
        {
            tearBarOnPlayerScript.currentTear -= 10f * Time.deltaTime; // 100 tearBar points / WaitForSeconds(10) after starting the Coroutine = 5

        }

    }
}
