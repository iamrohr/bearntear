using UnityEngine;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;
    public static PersistentObject Instance { get { return instance; } }

    public static Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }
}
