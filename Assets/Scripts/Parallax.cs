using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private float length;
    private float startPos;

    public float parallaxEffect;

    public GameObject mainCamera;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        //float temp = mainCamera.transform.position.x * (1 - parallaxEffect);
        float distance = (mainCamera.transform.position.x * parallaxEffect);

        //if(mainCamera.transform.position.x < -14.63608f)
        //{
            transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        //}


        //if (temp > startPos + length)
        //{
        //    startPos += length;
        //}

        //else if(temp < startPos - length)
        //{
        //    startPos -= length;
        //}
    }

}
