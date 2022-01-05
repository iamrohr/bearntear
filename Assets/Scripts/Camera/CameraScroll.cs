using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScroll : MonoBehaviour
{
    public float smoothFloat;
    float yOffset;


    void Start()
    {
        yOffset = 50f;
    }

    
    void FixedUpdate()
    {
        //transform.position.y <= -0.38f &&

        if (transform.position.y >= -54.57f)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y - yOffset, transform.position.z), smoothFloat);
        }
    }
}
