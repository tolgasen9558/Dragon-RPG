using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
public class PlayerMovement : MonoBehaviour
{

	[SerializeField] float walkMoveStopRadius = 0.2f;

	ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentClickTarget;

	bool isInDirectMode = false; 

    private void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        currentClickTarget = transform.position;
    }

	//TODO: Fix issue with click to move and WSAD conflicting and increasing speed

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
		if(Input.GetKeyDown(KeyCode.G)){	//TODO:G for gamepad. add to menu
			isInDirectMode = !isInDirectMode;
			currentClickTarget = transform.position;	//Clear the click target when toggling
		}

		if(isInDirectMode){
			ProcessDirectMovement();
		}
		else{
			ProcessMouseMovement(); 
		}
    }

	private void ProcessDirectMovement(){
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");


		Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
		Vector3 movement = v * cameraForward + h * Camera.main.transform.right;

		thirdPersonCharacter.Move(movement, false, false);
	}

	private void ProcessMouseMovement(){
		if(Input.GetMouseButton(1)){
			switch(cameraRaycaster.currentLayerHit){
				case Layer.Walkable:
					currentClickTarget = cameraRaycaster.hit.point;
					break;
				case Layer.Enemy:
					print("Not moving to enemy");
					break;
				default:
					print("Unexpected layer to move");
					return;
			}
		}
		var playerToClickPoint = currentClickTarget - transform.position;
		if(playerToClickPoint.magnitude >= walkMoveStopRadius){
			thirdPersonCharacter.Move(playerToClickPoint, false, false);
		}
		else{
			thirdPersonCharacter.Move(Vector3.zero, false, false);
		}
	}
}

