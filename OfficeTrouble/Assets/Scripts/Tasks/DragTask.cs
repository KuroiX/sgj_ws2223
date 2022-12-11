using System;
using UnityEngine;

public class DragTask : GenericTask
{
	
	[SerializeField] private string draggableObjectName;
	[SerializeField] private string triggerName;

	private GameObject _draggableObject;
	private GameObject _dragTargetTrigger;
	private Vector3 _initialPosition;

	private new void Start()
	{
		base.Start();
		_draggableObject = GameObject.Find(draggableObjectName).gameObject;
		_dragTargetTrigger = GameObject.Find(triggerName).gameObject;
		_initialPosition = _draggableObject.transform.position - (Vector3) _draggableObject.GetComponentInChildren<DraggableObject>().offset;
	}
	
	protected override void SpecificUpdate()
	{

	}

	protected override float CalculateTaskProgress()
	{
		Vector3 targetPosition = _dragTargetTrigger.transform.position;
		Vector3 currentPosition = _draggableObject.transform.position - (Vector3) _draggableObject.GetComponentInChildren<DraggableObject>().offset;
		float progress = 1 - Mathf.Min(1f, (targetPosition - currentPosition).magnitude / (targetPosition - _initialPosition).magnitude);
		Debug.Log("targetPos: " + targetPosition + ", currentPos: " + currentPosition + ", distance: " + (targetPosition - currentPosition).magnitude + ", full distance: " + (targetPosition - _initialPosition).magnitude + ", progress: " + progress);
		return progress;
	}

	protected override void OnKeyPressed()
	{
		if (_draggableObject.GetComponentInChildren<DraggableObject>().GetHooked())
			TaskIsBeingDealtWith = true;
	}

	protected override void OnKeyUnpressed()
	{
		if (TaskIsBeingDealtWith)
		{
			TaskIsBeingDealtWith = false;
			Debug.Log("MAKE RAYCAST TARGET CODE! Object: " + draggableObjectName + ", target: " + triggerName);
			if (CalculateTaskProgress() >= 0.8f)
				TaskFulfilled = true;
		}
	}

}
