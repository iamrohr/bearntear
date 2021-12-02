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
            StartCoroutine(cameraShake.Shake(0.3f, 0.2f));
        }
    }
}
