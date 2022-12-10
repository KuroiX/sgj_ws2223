using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    
    #region Variables
    
    private const float StressDecayPerTick = 0.05f;

    [SerializeField] private Sequence sequence;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private PanikBar panikBar;
    
    public StressMeter StressMeter;
    
    private List<GenericTask> _activeTasks;
    private int _currentTaskIndex;
    private bool _allTasksAreBeingDealtWith;

    #endregion
    
    #region Event Functions

    private void Awake()
    {
        StressMeter = new StressMeter();
        
    }

    private void Start()
    {
        _activeTasks = new List<GenericTask>();
        _currentTaskIndex = 0;
        if (sequence.tasks.Count > 0)
            StartCoroutine(ActivateNextTask());
        panikBar.Register(StressMeter);
        AudioManagerScript.Instance.Register(StressMeter);
    }

    private void Update()
    {

        List<GenericTask> tasksToRemove = new List<GenericTask>();
        
        _allTasksAreBeingDealtWith = true;
        foreach (GenericTask task in _activeTasks)
        {
            if (task.GetTaskFulfilled())
            {
                tasksToRemove.Add(task);
            }
            else if (!task.GetTaskIsBeingDealtWith())
                _allTasksAreBeingDealtWith = false;
        }

        foreach (GenericTask task in tasksToRemove)
        {
            _activeTasks.Remove(task);
            Destroy(task.gameObject);
        }

        Debug.Log(_allTasksAreBeingDealtWith);
    }

    private void FixedUpdate()
    {
        if (_allTasksAreBeingDealtWith)
            StressMeter.DecreaseStressLevel(StressDecayPerTick);
    }

    #endregion
    
    #region Coroutines
    
    private IEnumerator ActivateNextTask()
    {
        TaskSchedule currentTaskSchedule = sequence.tasks.ElementAt(_currentTaskIndex);
        GenericTask currentTask = currentTaskSchedule.task;

        GameObject currentTaskGameObject = Instantiate(currentTask.gameObject, canvasTransform);
        currentTaskGameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentTask.GetKeyName().ToUpper();
        
        _activeTasks.Add(currentTaskGameObject.GetComponent<GenericTask>());

        if (_currentTaskIndex + 1 < sequence.tasks.Count)
        {
            float delaySeconds = sequence.tasks[_currentTaskIndex + 1].timeStamp - sequence.tasks[_currentTaskIndex].timeStamp;
            yield return new WaitForSeconds(delaySeconds);
            _currentTaskIndex++;
            StartCoroutine(ActivateNextTask());
        }
        else
            Debug.Log("sequence over");
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
