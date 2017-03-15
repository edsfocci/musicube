using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AndroidAutofocuser : MonoBehaviour, ITrackableEventHandler
{
	#if UNITY_ANDROID
	private TrackableBehaviour imageTarget;
	private bool isTracking;

	void Start()
	{
		imageTarget = GetComponent<TrackableBehaviour>();

		if (imageTarget)
		{
			imageTarget.RegisterTrackableEventHandler(this);
		}

		SetCameraFocus();
	}
	#endif

	public void OnTrackableStateChanged( TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus )
	{
		#if UNITY_ANDROID
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			isTracking = true;
		}
		else
		{
			isTracking = false;
		}
		#endif
	}

	#if UNITY_ANDROID
	void SetCameraFocus()
	{
		if (!isTracking)
		{
			CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);		
		}
		Invoke("SetCameraFocus", 2f);
	}
	#endif
}



