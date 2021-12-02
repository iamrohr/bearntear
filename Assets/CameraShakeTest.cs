using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeTest : MonoBehaviour
{
    public CameraShake cameraShake;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)) 
        {
            StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
        }
    }
}
