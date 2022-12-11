using System;
using UnityEngine;
using UnityEngine.UI;

public class DragTask : GenericTask
{
	
	[SerializeField] private string draggableObjectName;
	[SerializeField] private string triggerName;
	[SerializeField] private bool isTapeDragTask;	// true: is curtain task. False: is tape task.

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
			if (CalculateTaskProgress() >= 0.8f)
			{
				
				Debug.Log("TODO: MAKE TAPE OR CURTAIN FIX");
				
				string imageToDisable, imageToEnable;
				if (isTapeDragTask)
				{
					imageToEnable = "TapePipe";
					imageToDisable = "";
				}
				else
				{
					imageToEnable = "CurtainUp";
					imageToDisable = "CurtainDown";
				}
				GameObject.Find(imageToEnable).GetComponent<Image>().enabled = true;
				GameObject objectToDisable = GameObject.Find(imageToDisable);
				if (objectToDisable)
					objectToDisable.GetComponent<Image>().enabled = false;
				
				TaskFulfilled = true;
			}
		}
	}

}
