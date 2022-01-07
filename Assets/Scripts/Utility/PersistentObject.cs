using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentObject : MonoBehaviour
{
    private static PersistentObject instance;
    public static PersistentObject Instance { get { return instance; } }

    public static Transform _transform;
    public int lastScene;

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
