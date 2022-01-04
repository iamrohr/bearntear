using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPos;

    public float parallaxEffect;

    GameObject mainCameraGameObject;

    private void Start()
    {
        mainCameraGameObject = GameObject.FindGameObjectWithTag("MainCamera");
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        if(mainCameraGameObject == null)
        {
            return;
        }

        if (mainCameraGameObject.activeSelf)
        {
            float distance = (mainCameraGameObject.transform.position.x * parallaxEffect);

            transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        }

        
    }

}
