using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ResumeGame()
    {
        this.GetComponentInChildren<Canvas>().enabled = false;
        Time.timeScale = 1;
    }
}
