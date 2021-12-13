using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearMode : MonoBehaviour
{
    //public TearBarOnPlayer tearBarOnPlayerScript;
    public PlayerMovement playerMovementScript;
    Camera mainCamera;
    CameraShake cameraShake;

    public GameObject swipeAttack;

    public bool tearModeOn = false;

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
        // stop player from moving
        // stop enemies from moving

        yield return new WaitForSeconds(0.001f);

        mainCamera.orthographicSize = 5.382994f;
        Time.timeScale = 1f;
        playerMovementScript.speed = 12f;
        swipeAttack.GetComponent<PlayerAttackBox>().damage = 16;

        yield return new WaitForSeconds(20f);

        tearModeOn = false;
        playerMovementScript.speed = 6f;
        swipeAttack.GetComponent<PlayerAttackBox>().damage = 8;

        //Time.timeScale = 1;


        // move faster
    }


    private void Update()
    {
        //if (tearBarOnPlayerScript.currentTear == tearBarOnPlayerScript.maxTear)

            //if (Input.GetButton("TearMode"))
            //{
            //    StartCoroutine(TearModeStart());
            //}
    }
}
