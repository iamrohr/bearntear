using System;
using System.Collections;
using UnityEngine;

public class PlayerSlam : MonoBehaviour
{
    [SerializeField] private float slamHeight, slamTime, slamRecoveryTime, hangTime; 
    private Player player;
    private PlayerJump playerJump;
    private PlayerInput playerInput;
    private Transform _transform;
    private Rigidbody2D rb;
    private CameraShake cameraShake;
    [SerializeField] private GameObject slamAttack;
    [SerializeField] private AudioSource slamSound;

    private void Awake()
    {
        cameraShake = GameObject.Find("CameraHolder").GetComponent<CameraShake>();
        rb = GetComponentInParent<Rigidbody2D>();
        _transform = transform;
        player = GetComponent<Player>();
        playerJump = GetComponent<PlayerJump>();
        playerInput = GetComponent<PlayerInput>();
    }


    public void SlamUpdate()
    {
        //if (player.stage < 3) return; //TODO: FIX

        if (playerInput.attackDown)
        {
            player.playerSM.EnterState(PlayerState.Slamming);
            StartCoroutine(SlamAttack());
        }
    }

    private IEnumerator SlamAttack()
    {
        player.MakeInvulnerable(slamTime * 2 + hangTime);
        playerJump.CancelJump();
        rb.velocity = Vector2.zero;
        float groundedY = playerJump.groundedY;
        float t = 0, startY = _transform.localPosition.y;

        while (t < 1)
        {
            float tween = t * t;
            float y = _transform.localPosition.y;

            y = startY + slamHeight * tween;
            _transform.localPosition = new Vector2(_transform.localPosition.x, y);

            t += Time.deltaTime / slamTime;
            yield return null;
        }

        yield return new WaitForSeconds(hangTime);
        t = 0;
        startY = _transform.localPosition.y;
        float distance = startY - groundedY;

        while (t < 1)
        {
            float tween = t * t;
            float y = _transform.localPosition.y;

            y = startY - distance * tween;
            _transform.localPosition = new Vector2(_transform.localPosition.x, y);

            t += Time.deltaTime / slamTime;

            if (t > 1 - 0.15f / slamTime)
            {
                player.animator.SetTrigger("Slam");
            }
            yield return null;
        }

        slamSound.Play();

        _transform.localPosition = new Vector2(_transform.localPosition.x, groundedY);
        playerJump.grounded = true;

        StartCoroutine(cameraShake.Shake(0.3f, 0.4f));

        var attackObject = Instantiate(slamAttack, _transform.position, Quaternion.identity);
        attackObject.transform.SetParent(_transform);

        yield return new WaitForSeconds(slamRecoveryTime);
        player.playerSM.LeaveState(PlayerState.Slamming);

        yield return null;
    }
}
