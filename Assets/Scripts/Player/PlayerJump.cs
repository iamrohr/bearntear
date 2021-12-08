using System;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpHeight, hangTime, jumpTime;
    public AudioSource jumpSound;
    [NonSerialized] public bool grounded = true;
    [NonSerialized] public float groundedY;

    private Player player;
    private IEnumerator jump;

    void Start()
    {
        player = GetComponent<Player>();
        groundedY = transform.localPosition.y;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            if (jumpTime > 0)
            {
                jump = Jump();
                StartCoroutine(jump);
            }
        }
    }

    public void CancelJump()
    {
        StopCoroutine(jump);
    }

    private IEnumerator Jump()
    {
        player.EnterState(PlayerState.Jumping, jumpTime * 2);

        jumpSound.Play();

        float t = 0;
        float startY = groundedY;

        while (t < 1)
        {
            float tween = 1 - (1 - t) * (1 - t) * (1 - t); //Smooth stop
            float y = transform.localPosition.y;

            y = startY + jumpHeight * tween;
            transform.localPosition = new Vector2(transform.localPosition.x, y);

            t += Time.deltaTime / jumpTime;
            yield return null;
            if (player.state == PlayerState.Dashing)
                break;
        }

        yield return new WaitForSeconds(hangTime);

        t = 0;
        startY = transform.localPosition.y;

        while (t < 1 && transform.localPosition.y > groundedY)
        {
            if (player.state != PlayerState.Dashing)
            {
                float tween = t * t; //Smooth start
                float y = transform.localPosition.y;

                y = startY - jumpHeight * tween;
                transform.localPosition = new Vector2(transform.localPosition.x, y);

                t += Time.deltaTime / jumpTime;
            }

            yield return null;
        }

        transform.localPosition = new Vector2(transform.localPosition.x, groundedY);
        grounded = true;
        player.LeaveState(PlayerState.Jumping);
        yield return null;
    }
}