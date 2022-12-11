using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableObject : MonoBehaviour
{

	[SerializeField] private Vector2 offset;

	private bool _hooked;

	private EventSystem _eventSystem;
	private GraphicRaycaster _raycaster;
	private PointerEventData _pointerEventData;

	private void Start()
	{
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
				Debug.Log(result);
				if (result.gameObject == gameObject)
				{
					_hooked = true;
				}
			}
			
		}
		if (Input.GetKeyUp(KeyCode.Mouse0))
		{
			_hooked = false;

			if (_hooked)
			{
				// make raycast to trigger objects and trigger events
			}
		}

		if (_hooked)
		{
			transform.parent.position = Input.mousePosition + (Vector3) offset;
		}
	}
	

}
