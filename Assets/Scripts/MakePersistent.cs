using UnityEngine;

public class MakePersistent : MonoBehaviour
{
    private void Start()
    {
        if (PersistentObject._transform != null)
            transform.SetParent(PersistentObject._transform);
    }
}
