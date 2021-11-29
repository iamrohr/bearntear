using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpSpeed, jumpHeight, hangTime, jumpCurveFactor, minJumpHeight;

    private bool grounded = true;
    private float defaultYOffSet;

    void Start()
    {
        defaultYOffSet = transform.localPosition.y;
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
        var tempJumpSpeed = jumpSpeed;
        while (transform.localPosition.y < defaultYOffSet + jumpHeight
                && Input.GetButton("Jump")
                || transform.localPosition.y < defaultYOffSet + minJumpHeight)
        {
            transform.localPosition += (Vector3)Vector2.up * tempJumpSpeed * Time.deltaTime; // Time.deltaTime not need because not actually running in Update
            tempJumpSpeed /= jumpCurveFactor;
            Debug.Log(tempJumpSpeed);
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(hangTime); // hangtime in air
        Debug.Log("Going down");
        while (transform.localPosition.y >= defaultYOffSet)
        {
            transform.localPosition += (Vector3)Vector2.down * tempJumpSpeed * Time.deltaTime; // Time.deltaTime not need because not actually running in Update
            tempJumpSpeed *= jumpCurveFactor;
            Debug.Log(tempJumpSpeed);
            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = new Vector2(transform.localPosition.x, defaultYOffSet);
        grounded = true;
        StopCoroutine(nameof(Jump));
    }
}