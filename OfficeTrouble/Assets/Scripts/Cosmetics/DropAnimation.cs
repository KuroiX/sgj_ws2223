using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropAnimation : MonoBehaviour
{
    [SerializeField]
    private GameObject dropPrefab;

    [SerializeField]
    private Transform start, end;

    [SerializeField]
    private float timeToGrow, timeToDrop;

    private float startTime = 0;

    private void Awake()
    {
        this.dropPrefab = Instantiate(dropPrefab, start.position, Quaternion.identity, null);
        this.dropPrefab.transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (dropPrefab.transform.localScale.x == 1)
        {
            float progress = (Time.time - startTime) / timeToDrop;
            if(progress > 1)
            {
                dropPrefab.transform.position = start.position;
                dropPrefab.transform.localScale = Vector3.zero;
            } else
            {
                Vector3 distance = end.position - start.position;
                dropPrefab.transform.position = start.position + progress * progress * distance;
            }
        }
        else
        {
            dropPrefab.transform.localScale = Mathf.Min(1, dropPrefab.transform.localScale.x + Time.deltaTime / timeToGrow) * Vector3.one;

            startTime = Time.time;
        }

    }
}
