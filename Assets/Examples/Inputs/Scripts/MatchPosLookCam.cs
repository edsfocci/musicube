using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPosLookCam : MonoBehaviour {
	public Transform toMatch;
	
	// Update is called once per frame
	void Update () {
		transform.position = toMatch.position;
		transform.LookAt (Camera.main.transform.forward + transform.position);
	}
}
