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
    [SerializeField] private RectTransform canvasTransform;
    
    private List<GenericTask> _activeTasks;
    private int _currentTaskIndex;

    #endregion
    
    #region Event Functions
    
    private void Start()
    {
        _activeTasks = new List<GenericTask>();
        _currentTaskIndex = 0;
        if (sequence.tasks.Count > 0)
            StartCoroutine(ActivateNextTask());
    }

    private void Update()
    {

        foreach (GenericTask task in _activeTasks)
        {
            if (task.CheckTaskFulfilled())
            {
                _activeTasks.Remove(task);
                Destroy(task.gameObject);
            }
            
            /*Debug.Log(task.CheckTaskFulfilled());
            if (task.CheckTaskFulfilled())
            {
                Debug.Log("EY! TASK " + task + "DONE");
                if (task.correspondingAlert)
                    _activeKeyAlerts.Remove(task.correspondingAlert);
                Destroy(task.correspondingAlert);
                task.correspondingAlert = null;
            }*/
        }

    }

    #endregion
    
    #region Coroutines
    
    private IEnumerator ActivateNextTask()
    {
        TaskSchedule currentTaskSchedule = sequence.tasks.ElementAt(_currentTaskIndex);
        GenericTask currentTask = currentTaskSchedule.task;
        _activeTasks.Add(currentTask);

        GameObject taskPrefab = currentTask.gameObject;
        GameObject taskGameObject = Instantiate(taskPrefab, canvasTransform);
        taskGameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentTask.GetKeyName().ToUpper();

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
    
    #endregion
    
    #region Helper Functions
    
    /*private void ShowKeyAlert(GenericTask task)
    {
        GameObject newText = Instantiate(textPrefab, canvasTransform);
        newText.transform.position = new Vector3(task.GetXCoord() / 1.834862f, task.GetYCoord() / 1.834862f, 0f);   // no idea why this is multiplied with 1.834862 but i have to revert it
        newText.GetComponent<TextMeshProUGUI>().text = task.GetKeyValue();
        _activeKeyAlerts.Add(newText);
        task.correspondingAlert = newText;
    }*/

    #endregion
    
}

#endregion

#endregion
