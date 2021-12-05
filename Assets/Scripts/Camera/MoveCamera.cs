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
    public float clampedLeftPos = -32.5f;
    public float clampedRightPos = 32f;

    void Start()
    {
        
    }

    
    void FixedUpdate()
    {
        //convert target pos to 2D
        
        targetScreenPos = Camera.main.WorldToScreenPoint(targetPosition.position);

        if(targetPosition.position.x >= clampedLeftPos && targetPosition.position.x <= clampedRightPos)
        {
            if (targetScreenPos.x > (Screen.width / 2) + camWindowDimension)
            {
                deltaX = targetScreenPos.x - ((Screen.width / 2) + camWindowDimension);

                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z), smoothFloat);
            }
        }

        if(targetPosition.position.x >= clampedLeftPos && targetPosition.position.x <= clampedRightPos)
        {
            if (targetScreenPos.x < (Screen.width / 2) - camWindowDimension)
            {
                deltaX = targetScreenPos.x - ((Screen.width / 2) - camWindowDimension);

                transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x + deltaX, transform.position.y, transform.position.z), smoothFloat);
            }
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
