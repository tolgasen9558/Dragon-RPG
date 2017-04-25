using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (CameraRaycaster))]
public class CursorAffordance : MonoBehaviour {

	[SerializeField] Texture2D walkCursor = null;
	[SerializeField] Texture2D targetCursor = null;
	[SerializeField] Texture2D unknownCursor = null;

	[SerializeField] Vector2 cursorHotSpot = new Vector2(0, 0);

	private CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start () {
		cameraRaycaster = GetComponent<CameraRaycaster>();
		cameraRaycaster.onLayerChange += OnLayerChanged;
	}
	
	// Only called when layer changes
	void OnLayerChanged (Layer newLayer) {
		Texture2D currentCursor;
		switch(newLayer){
			case Layer.Walkable:
				currentCursor = walkCursor;
				break;
			case Layer.Enemy:
				currentCursor = targetCursor;
				break;
			case Layer.RaycastEndStop:
				currentCursor = unknownCursor;
				break;
			default:
				Debug.LogError("CursorAffordance: Clicked object not defined, cannot set cursor image!");
				return;
		}
		Cursor.SetCursor(currentCursor, cursorHotSpot, CursorMode.Auto);

		if(Input.GetMouseButtonDown(1)){
			//TODO:Write Click Function Here
		}
	}

	//TODO: Consider de-registering OnLayerChanged on leaving all game scenes
}
