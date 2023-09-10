using System;
using UnityEngine;
using UnityEngine.UI;

public class DragTask : GenericTask
{
	
	[SerializeField] private string draggableObjectName;
	[SerializeField] private string triggerName;
	[SerializeField] private int dragTaskId;	// true: is curtain task. False: is tape task.

	private GameObject _draggableParent;
	private GameObject _dragTargetTrigger;
	private Vector3 _initialPosition;

	private new void Start()
	{
		base.Start();
		_draggableParent = GameObject.Find(draggableObjectName).gameObject;
		_dragTargetTrigger = GameObject.Find(triggerName).gameObject;
		_initialPosition = _draggableParent.transform.GetChild(0).position;
	}
	
	protected override void SpecificUpdate()
	{

	}

	protected override float CalculateTaskProgress()
	{
		Vector3 targetPosition = _dragTargetTrigger.transform.position;
		Vector3 currentPosition = _draggableParent.transform.GetChild(0).position;
		float progress = 1 - Mathf.Min(1f, (targetPosition - currentPosition).magnitude / (targetPosition - _initialPosition).magnitude);
		//Debug.Log("targetPos: " + targetPosition + ", currentPos: " + currentPosition + ", distance: " + (targetPosition - currentPosition).magnitude + ", full distance: " + (targetPosition - _initialPosition).magnitude + ", progress: " + progress);
		return progress;
	}

	protected override void OnKeyPressed()
	{
		if (_draggableParent.GetComponentInChildren<DraggableObject>().GetHooked())
			TaskIsBeingDealtWith = true;
	}

	protected override void OnKeyUnpressed()
	{
		if (TaskIsBeingDealtWith)
		{
			TaskIsBeingDealtWith = false;
			if (CalculateTaskProgress() >= 0.8f)
			{

				string imageToDisable, imageToEnable;
				if (dragTaskId == 0)
				{
					imageToEnable = "TapePipe";
					imageToDisable = "";
				}
				else if (dragTaskId == 1)
				{
					imageToEnable = "CurtainUp";
					imageToDisable = "CurtainDown";
				} 
				else
				{
					imageToEnable = "PlanksCeiling";
					imageToDisable = "PlanksFloor";
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
