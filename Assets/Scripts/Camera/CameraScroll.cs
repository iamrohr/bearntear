using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScroll : MonoBehaviour
{
    Camera mainCamera;

    public IntroFadeInOut introFadeInOut;

    public float smoothFloat;
    public float duration = 5f;

    float yOffset;
    float elapsed = 0.0f;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        yOffset = 50f;
        introFadeInOut.IntroFadeIn();

    }

    
    void FixedUpdate()
    {
        if (transform.position.y >= -54.57f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z), smoothFloat);
        }

        if(transform.position.y >= -54.57f && transform.position.y <= -51f)
        {
            elapsed += Time.deltaTime / duration;

            mainCamera.orthographicSize = Mathf.Lerp(5.397272f, 4.5f, elapsed);
            introFadeInOut.IntroFadeOut();
            //mainCamera.orthographicSize = 5.2f;
        }

        if (transform.position.y <= - 54.5f)
        {
            SceneManager.LoadScene("Main");
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
