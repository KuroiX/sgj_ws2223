using System;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : ScriptableObject
{
    public List<TaskSpawner> tasks;
}

[Serializable]
public struct TaskSpawner
{
    public GenericTask task;
    public float timeStamp;
}
