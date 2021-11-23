using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementTEST : MonoBehaviour
{

    Rigidbody2D rbPlayer;
    public float speed;

    private void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rbPlayer.AddForce(direction.normalized * Time.deltaTime * speed);
    }

    
}
