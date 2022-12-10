using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask
{

    public void OnPenalty();

    public bool CheckTaskFulfilled();

    public void ProcessUpdate();

    public void StartTask();
}
