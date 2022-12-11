using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public AudioClip tuneUp;
    public AudioSource music;
    public void LoadScene(int i)
    {
        // Play Sound Effect
        music.Stop();
        AudioSource.PlayClipAtPoint(tuneUp, Camera.main.transform.position);

        StartCoroutine(DelayLoadScene(i));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private IEnumerator DelayLoadScene(int i)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(i);
    }
}
