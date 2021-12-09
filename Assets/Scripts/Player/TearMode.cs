using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TearMode : MonoBehaviour
{
    public TearBarOnPlayer tearBarOnPlayerScript;
    public Camera mainCamera;

    CameraShake cameraShake;

    private void Start()
    {
        cameraShake = GameObject.Find("CameraHolder").GetComponent<CameraShake>();

    }

    public IEnumerator TearModeStart()
    {
        mainCamera.orthographicSize = 4.0f;
        Time.timeScale = 0;
        // knockBack effect
        // playSound
        // particleEffect
        StartCoroutine(cameraShake.Shake(1f, 0.4f));

        yield return new WaitForSeconds(1f);


    }


        private void Update()
    {
        if (tearBarOnPlayerScript.currentTear == tearBarOnPlayerScript.maxTear)

            if (Input.GetButton("TearMode"))
            {
                StartCoroutine(TearModeStart());
            }
    }
}
