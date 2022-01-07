using UnityEngine;
using UnityEngine.SceneManagement;


public class Outro : MonoBehaviour
{
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
