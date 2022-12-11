using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Time.timeScale = 0;
            this.GetComponentInChildren<Canvas>().enabled = true;
        }
    }

    public void LoadScene(int i)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(i);
    }

    public void ResumeGame()
    {
        this.GetComponentInChildren<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
}
