using UnityEngine;

public class BossBunnyShoot : MonoBehaviour
{
    [SerializeField] private Transform hand, needle;
    private LineRenderer lr;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        lr.SetPositions(new Vector3[] { hand.localPosition, needle.localPosition });
    }
}
