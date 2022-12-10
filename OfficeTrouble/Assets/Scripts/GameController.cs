#region Regions

#region Imports

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#endregion

#region Classes

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
    
    #region Event Functions
    
    private void Start()
    {
        _currentTaskIndex = 0;
        StartCoroutine(ActivateNextTask());
    }

    private void Update()
    {

        foreach (GenericTask task in _activeTasks)
        {
            task.ProcessUpdate();
        }

    }

    #endregion
    
    #region Coroutines
    
    private IEnumerator ActivateNextTask()
    {
        GenericTask currentTask = tasks[_currentTaskIndex];
        _activeTasks.Add(currentTask);
        currentTask.StartTask();
        
        if (_currentTaskIndex + 1 < timestampsSeconds.Length)
        {
            float delaySeconds = timestampsSeconds[_currentTaskIndex + 1] - timestampsSeconds[_currentTaskIndex];
            yield return new WaitForSeconds(delaySeconds);
            _currentTaskIndex++;
            StartCoroutine(ActivateNextTask());
        }
        else
            Debug.Log("Wohoo you completed the sequence");
        
    }
    
    #endregion
    
}

#endregion

#endregion
