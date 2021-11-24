using UnityEngine;

public enum HorDirection {Right, Left};

public class PlayerMovement : MonoBehaviour
{
    public float horSpeed, vertSpeed, jumpSpeed;
    public HorDirection horDirection;

    private Rigidbody2D rb;
    private float moveLimiter = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        horDirection = HorDirection.Right;
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

        rb.velocity = new Vector2(x, y);
        
        if (x > 0)
            horDirection = HorDirection.Right;
        else if (x < 0)
            horDirection = HorDirection.Left;
    }
}