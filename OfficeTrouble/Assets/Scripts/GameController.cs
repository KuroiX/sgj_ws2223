using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    #region Variables
    
    private const float StressDecayPerTick = 0.05f;

    [SerializeField] private Sequence sequence;
    [SerializeField] private RectTransform canvasTransform;
    [SerializeField] private PanikBar panikBar;
    [SerializeField] private CatAnimator catAnimator;
    private float _elapsedTime;


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
        ScoreManager.Instance.score = Time.time;
        _activeTasks = new List<GenericTask>();
        _currentTaskIndex = 0;
        //if (sequence.tasks.Count > 0)
        //    StartCoroutine(ActivateNextTask());
        StartAllCoroutines();
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

		if (StressMeter.IsGameLost())
		{
            ScoreManager.Instance.score = Time.time - ScoreManager.Instance.score;
			Debug.Log("YOU LOST! " + ScoreManager.Instance.score);
            AudioManagerScript.Instance.GameIsLost();
            SceneManager.LoadScene("GameOver");
        }

        _elapsedTime += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        int multiplier = 1;
        if (_elapsedTime > 1)
        {
            multiplier = 2;
        }
        else if (_elapsedTime > 2)
        {
            multiplier = 3;
        }
        
        if (_allTasksAreBeingDealtWith)
            StressMeter.DecreaseStressLevel(StressDecayPerTick * multiplier);
    }

    #endregion
    
    #region ReGIonFOrOnEmeThOD xD

    private void StartAllCoroutines()
    {
        for (int i = 0; i < sequence.tasks.Count; i++)
        {
            var schedule = sequence.tasks[i];
            StartCoroutine(StartTaskAtTimestamp(i, schedule.timeStamp));
            var task = schedule.task;
            var cat = task.catTaskInfo;
            if (!ReferenceEquals(cat, null) && cat.catExists)
            {
                StartCoroutine(StartCatAtTimestamp(i, schedule.timeStamp - cat.timeBeforeTask));
            }
        }
    }

    #endregion
    
    #region Coroutines

    private IEnumerator StartTaskAtTimestamp(int index, float timestamp)
    {
        yield return new WaitForSeconds(timestamp);
        
        GenericTask currentTask = sequence.tasks[index].task;
        
        GameObject currentTaskGameObject = Instantiate(currentTask.gameObject, canvasTransform);
        currentTaskGameObject.GetComponentInChildren<TextMeshProUGUI>().text = currentTask.GetKeyName().ToUpper();
        
        _activeTasks.Add(currentTaskGameObject.GetComponent<GenericTask>());
    }
    
    private IEnumerator StartCatAtTimestamp(int index, float timestamp)
    {
        yield return new WaitForSeconds(timestamp);
        var schedule = sequence.tasks[index];
        var catInfo = schedule.task.catTaskInfo;
        
        catAnimator.PlayPath(catInfo.path);
    }
    
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
    
 }
