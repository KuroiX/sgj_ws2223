using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    
    #region Constants
    
    [SerializeField]
    private GenericTask[] tasks;   // task sequence
    [SerializeField]
    private float[] timestampsSeconds;
    
    #endregion
    
    #region Variabes

    private List<GenericTask> _activeTasks;
    private uint _currentTaskIndex;

    #endregion
    
    private void Start()
    {
        _currentTaskIndex = 0;
        StartCoroutine(Delay());
    }

    private void Update()
    {

        foreach (GenericTask task in _activeTasks)
        {
            // task. process update
        }

    }

    private IEnumerator Delay()
    {
        GenericTask currentTask = tasks[_currentTaskIndex];
        _activeTasks.Add(currentTask);
        StartTask(currentTask);
        
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

    private void StartTask(GenericTask task)
    {
        Debug.Log("Started Task \"" + task + "\"");
    }

    #endregion

}
