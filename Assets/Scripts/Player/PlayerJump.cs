using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpSpeed, jumpHeight, hangTime, jumpCurveFactor, minJumpHeight;

    private bool grounded = true;
    private float defaultYOffSet;
    private PlayerMovement pm;
    private Player player;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
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
            yield return new WaitForEndOfFrame();
            if (player.state == PlayerState.Dashing)
                break;
        }

        yield return new WaitForSeconds(hangTime); // hangtime in air

        while (transform.localPosition.y >= defaultYOffSet)
        {
            if (player.state != PlayerState.Dashing)
            {
                transform.localPosition += (Vector3)Vector2.down * tempJumpSpeed * Time.deltaTime; // Time.deltaTime not need because not actually running in Update
                tempJumpSpeed *= jumpCurveFactor;
            }

            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = new Vector2(transform.localPosition.x, defaultYOffSet);
        grounded = true;
        StopCoroutine(nameof(Jump));
    }
}