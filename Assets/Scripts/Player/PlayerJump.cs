using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpSpeed;
    public float velocity;
    private bool grounded = true;
    public float jumpHeight = 2f;
    
    void Start()
    {

    }

    
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            StartCoroutine(Jump());

        }
       


    }

    private IEnumerator Jump()
    {
        while (transform.localPosition.y < 1.2f + jumpHeight)
        {
            transform.localPosition += (Vector3) Vector2.up * 0.2f; // Time.deltaTime not need because not actually running in Update

            yield return new WaitForEndOfFrame();
        }    

        yield return new WaitForSeconds(0.3f); // hangtime in air

        while (transform.localPosition.y > 1.2f)
        {
            
            transform.localPosition += (Vector3)Vector2.down * 0.2f; // Time.deltaTime not need because not actually running in Update

            yield return new WaitForEndOfFrame();
        }

        

        grounded = true;
    }

}
