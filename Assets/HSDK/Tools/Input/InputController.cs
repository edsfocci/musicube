using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {
	
	public LayerMask lMask;
	Transform headTransform;
	
	RaycastHit hit;
	Transform currentGazedObject;
	bool currentlyGazing = false;

	bool isTouchInput = false;
	// Use this for initialization
	void Start () 
	{
		headTransform = Camera.main.transform.parent;
	}
	public void SwitchInput(bool isTouchInputTp){
		isTouchInput = isTouchInputTp;
	}
	void Update ()
	{		
		Ray ray = new Ray ();
		if (isTouchInput) {
			ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		} else {
			ray.origin = headTransform.position;
			ray.direction = headTransform.forward;
		}
		if (Physics.Raycast (ray, out hit, 10f, lMask)) {			
			CheckGazing ();
			if (Input.GetMouseButtonDown (0)) {
				hit.transform.SendMessage ("OnClick", SendMessageOptions.DontRequireReceiver);
			}					
		} else {
			if (currentlyGazing) {
				HoveEnd ();
			} 
		}		
	}
	void CheckGazing(){
		if (!isTouchInput) {
			if (!currentlyGazing) {
				HoverStart ();
			} else {
				if (currentGazedObject != hit.transform) {
					HoveEnd ();
					HoverStart ();
				}
			}
		}
	}
	void HoverStart(){
		currentlyGazing = true;
		hit.transform.SendMessage ("OnHoverStart", SendMessageOptions.DontRequireReceiver);
		currentGazedObject = hit.transform;
		if (PointerControl.Ins != null) {
			PointerControl.Ins.ZoomUp ();
		}
	}
	void HoveEnd(){
		currentlyGazing = false;
		currentGazedObject.transform.SendMessage ("OnHoverEnd", SendMessageOptions.DontRequireReceiver);
		if (PointerControl.Ins != null) {
			PointerControl.Ins.ZoomToDefault ();
		}
	}
}
