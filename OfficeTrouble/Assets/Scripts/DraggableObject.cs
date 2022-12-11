using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour
{

	private bool _hooked;

	private EventSystem _eventSystem;
	private GraphicRaycaster _raycaster;
	private PointerEventData _pointerEventData;
	private Vector3 _initialPosition;
	private Vector3 _positionOffset;

	private void Start()
	{
		_initialPosition = transform.parent.position;
		_positionOffset = transform.position - transform.parent.position;
		_eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
		_raycaster = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			_pointerEventData = new PointerEventData(_eventSystem)
			{
				position = Input.mousePosition
			};
			List<RaycastResult> raycastResults = new List<RaycastResult>();
			_raycaster.Raycast(_pointerEventData, raycastResults);

			foreach (RaycastResult result in raycastResults)
			{
				if (result.gameObject == gameObject)
				{
					_hooked = true;
				}
			}
			
		}
		
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			_hooked = false;
			StartCoroutine(ScheduleResetPosition());
		}

		if (_hooked)
		{
			transform.parent.position = Input.mousePosition - _positionOffset;
		}
	}

	private IEnumerator ScheduleResetPosition()
	{
		yield return null;
		ResetPosition();
	}

	private void ResetPosition()
	{
		transform.parent.position = _initialPosition;
	}

	public bool GetHooked()
	{
		return _hooked;
	}


}
