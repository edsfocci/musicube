using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBoxManager : MonoBehaviour 
{
	public AudioSource boomBoxAudio;
	public AudioClip[] stations;
	public AudioClip buttonSFX;

	private int stationIndex = 0;

	public Animator boomBoxAnimator;
	public Animator volumeUpAnimator;
	public Animator volumeDownAnimator;

	public Transform headTransform;
	public LayerMask lMask;

	public Transform volumeUpButton;
	public Transform volumeDownButton;

	public BasicTrackableEventHandler imageTarget;

	void Start()
	{
		imageTarget.OnTrackingFound += HandleTrackingFound;
		imageTarget.OnTrackingLost += HandleTrackingLost;

		if (imageTarget.isTracking)
		{
			HandleTrackingFound();
		}

		ChangeStation();
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;

			if( Physics.Raycast( headTransform.position, headTransform.forward, out hit, 100f, lMask ))
			{
				if (hit.transform == volumeUpButton)
				{
					NextStation();
					volumeUpAnimator.Play("VolumeButtonPress");
					boomBoxAudio.PlayOneShot(buttonSFX);
				}
				else if (hit.transform == volumeDownButton)
				{
					PreviousStation();
					volumeDownAnimator.Play("VolumeButtonPress");
					boomBoxAudio.PlayOneShot(buttonSFX);
				}
			}
		}
	}

	void NextStation()
	{
		stationIndex++;

		if (stationIndex > stations.Length - 1)
		{
			stationIndex = 0;
		}

		ChangeStation();
	}

	void PreviousStation()
	{
		stationIndex--;

		if (stationIndex < 0 )
		{
			stationIndex = stations.Length - 1;
		}

		ChangeStation();
	}

	void ChangeStation()
	{
		boomBoxAudio.clip = stations[stationIndex];
		boomBoxAudio.Play();
	}
		
	void HandleTrackingFound()
	{
		boomBoxAudio.UnPause();
	}

	void HandleTrackingLost()
	{
		boomBoxAudio.Pause();
	}
}
