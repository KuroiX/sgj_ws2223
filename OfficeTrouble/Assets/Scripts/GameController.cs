using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    [SerializeField]
    private char[] tasks;
    [SerializeField]
    private float[] timestampsSeconds;

    private uint _currentTaskIndex;
    
    private void Start()
    {
        _currentTaskIndex = 0;
        StartCoroutine(Delay());
    }

    private void Update()
    {
        
        
        
    }

    private IEnumerator Delay()
    {
        StartTask(tasks[_currentTaskIndex]);
        
        if (_currentTaskIndex + 1 < timestampsSeconds.Length)
        {
            float delay = timestampsSeconds[_currentTaskIndex + 1] - timestampsSeconds[_currentTaskIndex];
            yield return new WaitForSeconds(delay);
            _currentTaskIndex++;
            StartCoroutine(Delay());
        }
        else
            Debug.Log("Wohoo you completed the sequence");
        
    }
    
    
    #region dummy functions

    private void StartTask(char task)
    {
        Debug.Log("Started Task \"" + task + "\"");
    }

    #endregion

}
