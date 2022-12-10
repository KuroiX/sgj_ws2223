using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void LoadScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}
