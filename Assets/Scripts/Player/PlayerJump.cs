using System;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpHeight, hangTime, jumpTime;
    public AudioSource jumpSound;
    [NonSerialized] public bool grounded = true;

    private float defaultStartY;
    private Player player;

    void Start()
    {
        player = GetComponent<Player>();
        defaultStartY = transform.localPosition.y;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
        {
            grounded = false;
            if (jumpTime > 0)
                StartCoroutine(Jump());
        }
    }

    private IEnumerator Jump()
    {
        player.EnterState(PlayerState.Jumping, jumpTime * 2);

        jumpSound.Play();

        float t = 0;
        float localStartY = defaultStartY;

        while (t < 1)
        {
            float smoothFactor = SmoothStop(t);
            float y = transform.localPosition.y;

            y = localStartY + jumpHeight * smoothFactor;
            transform.localPosition = new Vector2(transform.localPosition.x, y);

            t += Time.deltaTime / jumpTime;
            yield return new WaitForEndOfFrame();
            if (player.state == PlayerState.Dashing)
                break;
        }

        yield return new WaitForSeconds(hangTime);

        t = 0;
        localStartY = transform.localPosition.y;

        while (t < 1 && transform.localPosition.y > defaultStartY)
        {
            if (player.state != PlayerState.Dashing)
            {
                float smoothFactor = SmoothStart(t);
                float y = transform.localPosition.y;

                y = localStartY - jumpHeight * smoothFactor;
                transform.localPosition = new Vector2(transform.localPosition.x, y);

                t += Time.deltaTime / jumpTime;
            }

            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = new Vector2(transform.localPosition.x, defaultStartY);
        grounded = true;
        player.LeaveState(PlayerState.Jumping);
    }

    private float SmoothStart(float t)
    {
        return t * t;
    }

    private float SmoothStop(float t)
    {
        return 1 - (1 - t) * (1 - t) * (1 - t);
    }
}