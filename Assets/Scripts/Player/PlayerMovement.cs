using UnityEngine;

public enum HorDirection {Right, Left};

public class PlayerMovement : MonoBehaviour
{
    public float horSpeed, vertSpeed;
    public HorDirection horDirection;
    public bool isDashing = false;

    private Rigidbody2D rb;
    private float moveLimiter = 0.7f;

    void Start()
    {
        horDirection = HorDirection.Right;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * horSpeed;
        float y = Input.GetAxisRaw("Vertical") * vertSpeed;

        if (x != 0 && y != 0)
        {
            x *= moveLimiter;
            y *= moveLimiter;
        }

        if (!isDashing)
            rb.velocity = new Vector2(x, y);
        
        if (x > 0)
            horDirection = HorDirection.Right;
        else if (x < 0)
            horDirection = HorDirection.Left;
    }
}