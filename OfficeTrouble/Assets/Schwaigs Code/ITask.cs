using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask
{

    public void OnUncompleted();

    public bool CheckTaskFulfilled();

}
