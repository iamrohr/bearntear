using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveCamera : MonoBehaviour
{
    Transform targetPosition;
    public float camWindowDimension;

    Vector2 targetScreenPos;
    float deltaX;
    float deltaY;

    public float smoothFloat;
    public float clampedLeftPos;
    public float clampedRightPos;

    CameraSpawnLock cameraSpawnLock;

    void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform;

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Main"))
        {
            clampedLeftPos = -49.5f;
            clampedRightPos = 142f;
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("RoofTop"))
        {
            clampedLeftPos = -43.6f;
            clampedRightPos = 131f;
            transform.position = new Vector3(transform.position.x, 0.3f, transform.position.z);
            cameraSpawnLock = gameObject.GetComponent<CameraSpawnLock>();
            cameraSpawnLock.enabled = false;
        }

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Sewer"))
        {
            clampedLeftPos = -65.8909f;
            clampedRightPos = -20f;
            transform.position = new Vector3(transform.position.x, -1.08f, transform.position.z);
            cameraSpawnLock = gameObject.GetComponent<CameraSpawnLock>();
            cameraSpawnLock.enabled = false;
        }


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
