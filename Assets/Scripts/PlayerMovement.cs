using System;
using UnityEngine;

public enum AttackDirection {Right, Left};

public class PlayerMovement : MonoBehaviour
{
    public float horSpeed, vertSpeed, jumpSpeed;
    public AttackDirection attackDirection;

    private Rigidbody2D rb;
    private float moveLimiter = 0.7f;
    private bool grounded = true;

    // Start is called before the first frame update
    void Start()
    {
        attackDirection = AttackDirection.Right;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * horSpeed;
        float y = Input.GetAxisRaw("Vertical") * vertSpeed;

        if (x != 0 && y != 0)
        {
            x *= moveLimiter;
            y *= moveLimiter;
        }

        if (x > 0)
            attackDirection = AttackDirection.Right;
        else if (x < 0)
            attackDirection = AttackDirection.Left;

        Vector2 tempVelocity = rb.velocity;
        tempVelocity.x = x;
        if (rb.gravityScale == 0)
            tempVelocity.y = y;

        rb.velocity = tempVelocity;

        if (Input.GetButtonDown("Jump"))
            Jump();
    }

    private void Jump()
    {
        rb.gravityScale = 1;
        //rb.AddForce(new Vector2(0, jumpForce));

        Vector2 tempVelocity = rb.velocity;
        tempVelocity.y = jumpSpeed;
        rb.velocity = tempVelocity;
        grounded = false;
        float airTime = 2 * jumpSpeed / Mathf.Abs(Physics.gravity.y);
        Invoke(nameof(LogVelocity), 0.1f);
        Invoke(nameof(TurnOffGravity), airTime);
        //t = 2 * v / 9.81
    }

    private void LogVelocity()
    {
        Debug.Log(rb.velocity);
    }

    private void TurnOffGravity()
    {
        rb.gravityScale = 0;
    }
}