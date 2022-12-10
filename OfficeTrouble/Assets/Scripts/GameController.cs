#region Regions

#region Imports

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

#endregion

#region Classes

public class GameController : MonoBehaviour
{
    
    #region Constants
    
    [SerializeField] private GenericTask[] tasks;   // task sequence
    [SerializeField] private float[] timestampsSeconds;
    [SerializeField] private GameObject textPrefab;

    private uint _currentTaskIndex;
    private List<GenericTask> _activeTasks;
    private List<GameObject> _activeKeyAlerts;

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
        ShowKeyAlert(currentTask);
        
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
    
    #region Helper Functions
    
    private void ShowKeyAlert(GenericTask task)
    {

        GameObject newText = Instantiate(textPrefab, new Vector3(task.GetXCoord(), task.GetYCoord(), 0f), Quaternion.identity);
        newText.GetComponent<TextMeshPro>().text = task.GetKeyValue();
        _activeKeyAlerts.Add(newText);

    }

    #endregion
    
}

#endregion

#endregion
