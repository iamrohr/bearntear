using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    public GameObject attackHitbox;
    private float offsetValue = 1;

    private PlayerMovement pm;

    // Start is called before the first frame update
    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (pm.horDirection == HorDirection.Left)
        {
            Debug.Log("True");
        }
        Vector2 attackPos = new Vector2(transform.position.x, transform.position.y);
        Instantiate(attackHitbox, attackPos, Quaternion.identity);
    }
}
