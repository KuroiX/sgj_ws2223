#region Regions

#region Imports

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

#endregion

#region Classes

public class GameController : MonoBehaviour
{
    
    #region Constants

    [SerializeField] private Sequence sequence;
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private Transform canvasTransform;
    
    private int _currentTaskIndex;
    private List<GenericTask> _activeTasks;
    private List<GameObject> _activeKeyAlerts;

    #endregion
    
    #region Event Functions
    
    private void Start()
    {
        _currentTaskIndex = 0;
        _activeTasks = new List<GenericTask>();
        _activeKeyAlerts = new List<GameObject>();
       //StartCoroutine(ActivateNextTask());
       
       
       if (sequence.tasks.Count > 0)
       {
           TaskSchedule currentTaskSchedule = sequence.tasks.ElementAt(_currentTaskIndex);
           _activeTasks.Add(currentTaskSchedule.task);
           currentTaskSchedule.task.StartTask();
           ShowKeyAlert(currentTaskSchedule.task);
            
           if (_currentTaskIndex + 1 < sequence.tasks.Count)
           {
               float delaySeconds = sequence.tasks[_currentTaskIndex + 1].timeStamp - sequence.tasks[_currentTaskIndex].timeStamp;
               //yield return new WaitForSeconds(delaySeconds);
               _currentTaskIndex++;
               StartCoroutine(ActivateNextTask());
           }
           else
               Debug.Log("Wohoo you completed the sequence");
       }
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
        if (sequence.tasks.Count > 0)
        {
            TaskSchedule currentTaskSchedule = sequence.tasks.ElementAt(_currentTaskIndex);
            _activeTasks.Add(currentTaskSchedule.task);
            currentTaskSchedule.task.StartTask();
            ShowKeyAlert(currentTaskSchedule.task);
            
            if (_currentTaskIndex + 1 < sequence.tasks.Count)
            {
                float delaySeconds = sequence.tasks[_currentTaskIndex + 1].timeStamp - sequence.tasks[_currentTaskIndex].timeStamp;
                yield return new WaitForSeconds(delaySeconds);
                _currentTaskIndex++;
                StartCoroutine(ActivateNextTask());
            }
            else
                Debug.Log("Wohoo you completed the sequence");
        }
    }
    
    #endregion
    
    #region Helper Functions
    
    private void ShowKeyAlert(GenericTask task)
    {
        GameObject newText = Instantiate(textPrefab, new Vector3(task.GetXCoord(), task.GetYCoord(), 0f), Quaternion.identity);
        newText.transform.parent = canvasTransform;
        newText.GetComponent<TextMeshProUGUI>().text = task.GetKeyValue();
        _activeKeyAlerts.Add(newText);
    }

    #endregion
    
}

#endregion

#endregion
