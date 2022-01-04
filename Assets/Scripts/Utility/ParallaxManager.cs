using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    Camera mainCamera;

    Transform cameraTransform;

    GameObject parallaxBACK3;
    GameObject parallaxBACK4;
    GameObject parallaxBACK5;
    GameObject parallaxFRONT3;
    GameObject parallaxFRONT4;
    GameObject parallaxFRONT5;
    GameObject parallaxMID3;
    GameObject parallaxMID4;
    GameObject parallaxMID5;

    private void Awake()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cameraTransform = mainCamera.transform;

        parallaxBACK3 = GameObject.Find("ParallaxBACK (3)");
        parallaxBACK4 = GameObject.Find("ParallaxBACK (4)");
        parallaxBACK5 = GameObject.Find("ParallaxBACK (5)");
        parallaxFRONT3 = GameObject.Find("ParallaxFRONT (3)");
        parallaxFRONT4 = GameObject.Find("ParallaxFRONT (4)");
        parallaxFRONT5 = GameObject.Find("ParallaxFRONT (5)");
        parallaxMID3 = GameObject.Find("ParallaxMID (3)");
        parallaxMID4 = GameObject.Find("ParallaxMID (4)");
        parallaxMID5 = GameObject.Find("ParallaxMID (5)");

        parallaxBACK3.transform.SetParent(cameraTransform);
        parallaxBACK4.transform.SetParent(cameraTransform);
        parallaxBACK5.transform.SetParent(cameraTransform);
        parallaxFRONT3.transform.SetParent(cameraTransform);
        parallaxFRONT4.transform.SetParent(cameraTransform);
        parallaxFRONT5.transform.SetParent(cameraTransform);
        parallaxMID3.transform.SetParent(cameraTransform);
        parallaxMID4.transform.SetParent(cameraTransform);
        parallaxMID5.transform.SetParent(cameraTransform);


    }

    

    
}
