using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private List<GenericTask> Tasks;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var t in Tasks)
        {
            t.StartTask();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var t in Tasks)
        {
            t.ProcessUpdate();
        }
    }
}
