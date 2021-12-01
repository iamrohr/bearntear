using System;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpSpeed, jumpHeight, hangTime, jumpCurveFactor, minJumpHeight, jumpTime;

    private bool grounded = true;
    private float localStartY;
    private PlayerMovement pm;
    private Player player;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
        player = GetComponent<Player>();
        localStartY = transform.localPosition.y;
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
        float t = 0;
        float distance = localStartY + jumpHeight - transform.localPosition.y;
        while (transform.localPosition.y < localStartY + jumpHeight
                && Input.GetButton("Jump")
                || transform.localPosition.y < localStartY + minJumpHeight)
        {
            float smoothFactor = SmoothStop(t / jumpTime);
            float y = transform.localPosition.y;
            y = localStartY + distance * smoothFactor;
            transform.localPosition = new Vector2(transform.localPosition.x, y);
            t += Time.deltaTime;
            Debug.Log(localStartY + jumpHeight - transform.localPosition.y);
            yield return new WaitForEndOfFrame();
            if (player.state == PlayerState.Dashing)
                break;
        }

        //yield return new WaitForSeconds(hangTime); // hangtime in air

        //while (transform.localPosition.y >= defaultYOffSet)
        //{
        //    if (player.state != PlayerState.Dashing)
        //    {

        //    }

        //    yield return new WaitForEndOfFrame();
        //}

        //var tempJumpSpeed = jumpSpeed;
        //while (transform.localPosition.y < defaultYOffSet + jumpHeight
        //        && Input.GetButton("Jump")
        //        || transform.localPosition.y < defaultYOffSet + minJumpHeight)
        //{
        //    transform.localPosition += (Vector3)Vector2.up * tempJumpSpeed * Time.deltaTime;
        //    tempJumpSpeed /= jumpCurveFactor;
        //    yield return new WaitForEndOfFrame();
        //    if (player.state == PlayerState.Dashing)
        //        break;
        //}

        //yield return new WaitForSeconds(hangTime); // hangtime in air

        //while (transform.localPosition.y >= defaultYOffSet)
        //{
        //    if (player.state != PlayerState.Dashing)
        //    {
        //        transform.localPosition += (Vector3)Vector2.down * tempJumpSpeed * Time.deltaTime;
        //        tempJumpSpeed *= jumpCurveFactor;
        //    }

        //    yield return new WaitForEndOfFrame();
        //}

        //transform.localPosition = new Vector2(transform.localPosition.x, defaultYOffSet);
        grounded = true;
        StopCoroutine(nameof(Jump));
    }

    private float SmoothStop(float t)
    {
        return 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t);
    }
}