using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using UnityEngine.SceneManagement;

public class HSDK : MonoBehaviour 
{
	public Transform arCameraRef;

	private VideoBackgroundBehaviour leftVidBackBehaviour;
	private VideoBackgroundBehaviour rightVidBackBehaviour;
	bool isActive = false;

	void Start ()
	{
		//Set Mixr view.
		Invoke ("InitCameraView", .5f);
		Invoke ("ActiveSceneSwitch", .5f);
	}

	Color inActiveColor = new Color(1,1,1,0.157f);


	//Vuforia View Type AR/MIXR
	public UnityEngine.UI.Image btnSwitchViewSpritRef;
	bool isInDouble = true;

	public void SwitchView(){
		isInDouble = !isInDouble;
		if (isInDouble) {
			btnSwitchViewSpritRef.color = inActiveColor;
			SetToDoubleView ();
		} else {
			btnSwitchViewSpritRef.color = Color.white;
			SetToSingleView ();
		}
		btnSwitchViewSpritRef.gameObject.SetActive (false);
		Invoke ("EnableViewChangeBtn", 3f);
	}
	void EnableViewChangeBtn(){
		btnSwitchViewSpritRef.gameObject.SetActive (true);
	}
	void SetToSingleView(){
		Vuforia.DigitalEyewearARController.Instance.SetViewerActive (false);
	}
	void SetToDoubleView(){
		Vuforia.DigitalEyewearARController.Instance.SetViewerActive (true);
	}

	//FlashLight
	public UnityEngine.UI.Image btnFlashLightSpritRef;
	bool isFlashOn = false;

	public void SwitchFlashLight(){
		isFlashOn = !isFlashOn;
		if (isFlashOn) {
			btnFlashLightSpritRef.color = inActiveColor;
			TurnFlashOn ();
		} else {
			btnFlashLightSpritRef.color = Color.white;
			TurnFlashOff ();
		}
	}

	void TurnFlashOff(){
		Vuforia.CameraDevice.Instance.SetFlashTorchMode (false);
	}
	void TurnFlashOn(){
		Vuforia.CameraDevice.Instance.SetFlashTorchMode (true);
	}


	//Set Mixr View.
	void InitCameraView()
	{
		//StereoCameraLeft
		//StereoCameraRight
		Transform [] tps = arCameraRef.GetComponentsInChildren<Transform>();

		foreach (Transform tp in tps) 
		{
			if (tp.name == "StereoCameraLeft") 
			{
				leftVidBackBehaviour = tp.GetComponent<VideoBackgroundBehaviour>();
			}
			if (tp.name == "StereoCameraRight") 
			{
				rightVidBackBehaviour = tp.GetComponent<VideoBackgroundBehaviour>();
			}
		}

		if (leftVidBackBehaviour != null && rightVidBackBehaviour != null) 
		{
			isActive = true;
			Invoke ("DelayedSetup", 1f);
		} 
		else 
		{
			Invoke ("InitCameraView", .5f);
		}
	}

	void DelayedSetup()
	{
		CameraDevice.Instance.SetFocusMode (CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
		GameObject distMesh = GameObject.Find("DistortionMesh");

		distMesh.transform.localPosition = new Vector3(0f, -0.2f, 0f);
		distMesh.transform.localScale = new Vector3(.91f, .91f, .91f);
	}

	void LateUpdate()
	{
		if (isActive) 
		{
			leftVidBackBehaviour.SetStereoDepth (0);
			rightVidBackBehaviour.SetStereoDepth (0);
		}
	}


	//Btn Orientation
	DeviceOrientation currOrientation = DeviceOrientation.LandscapeRight;
	Vector3 leftOrientation = new Vector3(0,0,180f);
	Vector3 portraitPrientation = new Vector3(0,0,270f);
	Vector3 portraitUpDownOrientation = new Vector3(0,0,90f);

	void Update() 
	{
		if (currOrientation != Input.deviceOrientation) 
		{
			currOrientation = Input.deviceOrientation;
			UpdateOrientation ();
		}
	}

	void UpdateOrientation()
	{
		switch (currOrientation) 
		{
		case DeviceOrientation.LandscapeLeft:
			//up side down - 180
			btnFlashLightSpritRef.transform.localEulerAngles = leftOrientation;
			btnSwitchViewSpritRef.transform.localEulerAngles = leftOrientation;
			break;

		case DeviceOrientation.Portrait:
			//up side down - 270
			btnFlashLightSpritRef.transform.localEulerAngles = portraitPrientation;
			btnSwitchViewSpritRef.transform.localEulerAngles = portraitPrientation;
			break;

		case DeviceOrientation.PortraitUpsideDown:
			//up side down - 90
			btnFlashLightSpritRef.transform.localEulerAngles = portraitUpDownOrientation;
			btnSwitchViewSpritRef.transform.localEulerAngles = portraitUpDownOrientation;
			break;

		default:
			//up side down - 0
			btnFlashLightSpritRef.transform.localEulerAngles = Vector3.zero;
			btnSwitchViewSpritRef.transform.localEulerAngles = Vector3.zero;
			break;
		}
	}

	//Switch Scene
	public Text sceneName;
	bool isActiveSceneSwitch = false;

	void ActiveSceneSwitch()
	{
		sceneName.text = SceneManager.GetActiveScene ().name;
		isActiveSceneSwitch = true;
	}

	public void NextScene()
	{
		if (isActiveSceneSwitch) 
		{
			int index = SceneManager.GetActiveScene ().buildIndex;
			index++;

			if (index >= SceneManager.sceneCountInBuildSettings) 
			{
				index = 0;
			}

			SceneManager.LoadScene (index);
		}
	}

	public void PreviousScene()
	{
		if (isActiveSceneSwitch) 
		{
			int index = SceneManager.GetActiveScene ().buildIndex;
			index--;

			if (index < 0) 
			{
				index = SceneManager.sceneCountInBuildSettings - 1;
			}

			SceneManager.LoadScene (index);
		}
	}
}
