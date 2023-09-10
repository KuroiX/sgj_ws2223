using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textMeshPro;

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "GameOver")
        {
            ManageTimer();
        }
    }

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

    public void ManageTimer()
    {
        double roundedScore = System.Math.Round(ScoreManager.Instance.score, 1);
        textMeshPro.text = "" + roundedScore;
    }
}
