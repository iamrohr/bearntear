using UnityEngine;

public class PlayerAnimatorFunctions : MonoBehaviour
{
    private PlayerAttack playerAttack;

    private void Awake()
    {
        playerAttack = GetComponentInParent<PlayerAttack>();
    }

    public void Bash()
    {
        playerAttack.BashAttack();
    }
}
