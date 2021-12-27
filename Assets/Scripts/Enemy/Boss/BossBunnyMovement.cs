using UnityEngine;

public class BossBunnyMovement : MonoBehaviour
{
    [SerializeField] public LeftRight Facing { get; private set; }
    
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private Transform holderTransform;

    private void Awake()
    {
        holderTransform = GetComponentInParent<Transform>();
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Start()
    {
        Facing = LeftRight.Left;
    }

    [ContextMenu("Test")]
    public void Test()
    {

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
        speed -= direction.y * 0.5f * speed;
        Vector2 velocity = direction * (float)speed;

        rb.velocity = velocity;
        TurnToDirection();
    }

    private void TurnToDirection()
    {
        if (rb.velocity.x > 0 && Facing == LeftRight.Left)
        {
            Facing = LeftRight.Right;
            var x = Mathf.Abs(holderTransform.localScale.x) * -1;
            holderTransform.localScale = new Vector2(x, holderTransform.localScale.y);
        }
        else if (rb.velocity.x < 0 && Facing == LeftRight.Right)
        {
            Facing = LeftRight.Left;
            var x = Mathf.Abs(holderTransform.localScale.x);
            holderTransform.localScale = new Vector2(x, holderTransform.localScale.y);
        }
    }
}
