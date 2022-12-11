using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject canvas;
    
    private void Update()
    {
        if (Input.GetKey("escape"))
        {
            Time.timeScale = 0;
            canvas.SetActive(true);
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
        canvas.SetActive(false);
        Time.timeScale = 1;
    }
}
