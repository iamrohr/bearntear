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
    private Transform _transform;
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput= GetComponent<PlayerInput>();
        player = GetComponent<Player>();
        _transform = transform;
    }

    void Start()
    {
        groundedY = _transform.localPosition.y;
    }

    public void Jump()
    {
        if (playerInput.jump && grounded)
        {
            grounded = false;
            if (jumpTime > 0)
            {
                jump = JumpCoroutine();
                StartCoroutine(jump);
            }
        }
    }

    public void CancelJump()
    {
        StopCoroutine(jump);
    }

    private IEnumerator JumpCoroutine()
    {
        player.playerSM.EnterState(PlayerState.Jumping);
        player.MakeInvulnerable(jumpTime * 2);

        jumpSound.Play();

        float t = 0;
        float startY = groundedY;

        while (t < 1)
        {
            float tween = 1 - (1 - t) * (1 - t) * (1 - t); //Smooth stop
            float y = _transform.localPosition.y;

            y = startY + jumpHeight * tween;
            _transform.localPosition = new Vector2(_transform.localPosition.x, y);

            t += Time.deltaTime / jumpTime;
            yield return null;
            if (player.state == PlayerState.Dashing)
                break;
        }

        yield return new WaitForSeconds(hangTime);

        t = 0;
        startY = _transform.localPosition.y;

        while (t < 1 && _transform.localPosition.y > groundedY)
        {
            if (player.state != PlayerState.Dashing)
            {
                float tween = t * t; //Smooth start
                float y = _transform.localPosition.y;

                y = startY - jumpHeight * tween;
                _transform.localPosition = new Vector2(_transform.localPosition.x, y);

                t += Time.deltaTime / jumpTime;
            }

            yield return null;
        }

        _transform.localPosition = new Vector2(_transform.localPosition.x, groundedY);
        grounded = true;
        player.playerSM.LeaveState(PlayerState.Jumping);
        yield return null;
    }
}