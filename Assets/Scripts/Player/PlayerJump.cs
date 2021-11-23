using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpSpeed;
    public float velocity;
    private bool grounded = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            grounded = false;
            transform.Translate(Vector2.up * 5 * Time.deltaTime);
        }
        if (!grounded)
        {
            //transform.Translate(Vector2.down * 9.81f * Time.deltaTime);
        }
    }
}
