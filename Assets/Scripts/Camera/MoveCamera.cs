using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform targetPosition;
    public float camWindowDimension;

    Vector2 targetScreenPos;
    float deltaX;
    float deltaY;

    public float smoothFloat;
    public float clampedLeftPos;
    public float clampedRightPos;

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        //convert target pos to 2D

        targetScreenPos = Camera.main.WorldToScreenPoint(targetPosition.position);
        
            if (targetScreenPos.x > (Screen.width / 2) + camWindowDimension) // moving to the right 
            {
                deltaX = targetScreenPos.x - ((Screen.width / 2) + camWindowDimension);

                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z), smoothFloat);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampedLeftPos, clampedRightPos), transform.position.y, transform.position.z);
            }
        
            if (targetScreenPos.x < (Screen.width / 2) - camWindowDimension)
            {
                deltaX = targetScreenPos.x - ((Screen.width / 2) - camWindowDimension);

                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z), smoothFloat);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, clampedLeftPos, clampedRightPos), transform.position.y, transform.position.z);

            }
        

        

        //if (targetScreenPos.y > (Screen.height / 2) + camWindowDimension)
        //{
        //    deltaY = targetScreenPos.y - ((Screen.height / 2) + camWindowDimension);
            
        //    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + deltaY, transform.position.z), smoothFloat);

        //}

        //if (targetScreenPos.y < (Screen.height / 2) - camWindowDimension)
        //{
        //    deltaY = targetScreenPos.y - ((Screen.height / 2) - camWindowDimension);
            
        //    transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + deltaY, transform.position.z), smoothFloat);

        //}



    }
}
