using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpHeight, hangTime, /*minJumpHeight,*/ jumpTime;
    public AudioSource jumpSound;

    private bool grounded = true;
    private float defaultStartY;
    private PlayerMovement pm;
    private Player player;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
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
        jumpSound.Play();

        float localStartY = defaultStartY;
        float t = 0;
        float distance = localStartY + jumpHeight - transform.localPosition.y;
        while (transform.localPosition.y < localStartY + jumpHeight
                /*&& Input.GetButton("Jump")
                || transform.localPosition.y < localStartY + minJumpHeight*/)
        {
            float smoothFactor = SmoothStop(t / jumpTime);
            float y = transform.localPosition.y;
            y = localStartY + distance * smoothFactor;
            transform.localPosition = new Vector2(transform.localPosition.x, y);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
            if (player.state == PlayerState.Dashing)
                break;
        }

        yield return new WaitForSeconds(hangTime);

        t = 0;
        localStartY = transform.localPosition.y;
        while (transform.localPosition.y > defaultStartY)
        {
            if (player.state != PlayerState.Dashing)
            {
                float smoothFactor = SmoothStart(t / jumpTime);
                float y = transform.localPosition.y;
                y = localStartY - distance * smoothFactor;
                transform.localPosition = new Vector2(transform.localPosition.x, y);
                t += Time.deltaTime;
            }

            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = new Vector2(transform.localPosition.x, defaultStartY);
        grounded = true;
        StopCoroutine(nameof(Jump));
    }

    private float SmoothStart(float t)
    {
        return t * t;
    }

    private float SmoothStop(float t)
    {
        return 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t);
    }
}