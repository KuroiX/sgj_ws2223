using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MySequence", menuName = "ScriptableObject/Sequence", order = 100)]

public class Sequence : ScriptableObject
{
    public List<TaskSchedule> tasks;
}

[Serializable]
public struct TaskSchedule
{
    public GenericTask task;
    public float timeStamp;
}
