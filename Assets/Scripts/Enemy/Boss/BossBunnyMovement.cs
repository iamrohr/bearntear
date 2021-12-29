using System;
using UnityEngine;

public class BossBunnyMovement : MonoBehaviour
{
    [SerializeField] public LeftRight Facing { get; private set; }
    [NonSerialized] public Transform holderTransform;

    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("PlayerHolder").transform;
        holderTransform = transform.parent.transform;
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Start()
    {
        Facing = LeftRight.Left;
    }

    public void MoveTowards(GameObject gameObj, float? speed = null)
    {
        var targetPos = gameObj.transform.position;
        MoveTowards(targetPos, speed);
    }

    public void MoveTowards(Vector2 targetPos, float? speed = null)
    {
        if (speed == null)
            speed = this.speed;

        Vector2 direction = (targetPos - (Vector2)holderTransform.position).normalized;
        speed -= Mathf.Abs(direction.y) * 0.5f * speed;
        Vector2 velocity = direction * (float)speed;

        rb.velocity = velocity;
        TurnWithMovement();
    }

    private void TurnWithMovement()
    {
        if (rb.velocity.x > 0 && Facing == LeftRight.Left)
            Turn(LeftRight.Right);
        else if (rb.velocity.x < 0 && Facing == LeftRight.Right)
            Turn(LeftRight.Left);
    }

    public void TurnToPlayer()
    {
        if (holderTransform.position.x > playerTransform.position.x && Facing == LeftRight.Right)
            Turn(LeftRight.Left);
        else if (holderTransform.position.x < playerTransform.position.x && Facing == LeftRight.Left)
            Turn(LeftRight.Right);
    }
    
    public void Turn(LeftRight direction)
    {
        float x;
        switch (direction)
        {
            case LeftRight.Left:
                Facing = LeftRight.Left;
                x = Mathf.Abs(holderTransform.localScale.x);
                break;
            case LeftRight.Right:
                Facing = LeftRight.Right;
                x = Mathf.Abs(holderTransform.localScale.x) * -1;
                break;
            default:
                goto case LeftRight.Left;
        }
        holderTransform.localScale = new Vector2(x, holderTransform.localScale.y);
    }

    public (float x, float y) AxesDistancesToPlayer()
    {
        var distance = playerTransform.position - holderTransform.position;

        return (Mathf.Abs(distance.x), Mathf.Abs(distance.y));
    }

    public void StopMoving()
    {
        rb.velocity *= 0;
    }
}
