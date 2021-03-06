﻿using UnityEngine;
using Vuforia;

public class BasicTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
	
	private TrackableBehaviour mTrackableBehaviour;

	public delegate void TrackingEvent();
	public event TrackingEvent OnTrackingFound;
	public event TrackingEvent OnTrackingLost;

	[Tooltip("These are the objects that should disable when the tracker is lost and enable when the tracker is found.")]
	public GameObject[] objectsToHide;

	public bool isTracking { get; private set; }

	void Start()
	{
		isTracking = false;

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();

		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}

		OnTrackingFound += HandleTrackingFound;
		OnTrackingLost += HandleTrackingLost;
	}

	public void OnTrackableStateChanged( TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus )
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			isTracking = true;

			if (OnTrackingFound != null)
			{
				OnTrackingFound();
			}
		}
		else
		{
			isTracking = false;

			if (OnTrackingLost != null)
			{
				OnTrackingLost();
			}
		}
	}


	void HandleTrackingFound()
	{
		if (objectsToHide != null)
		{
			for (int index = 0; index < objectsToHide.Length; index++)
			{
				objectsToHide[index].SetActive(true);
			}
		}
			
	}

	void HandleTrackingLost()
	{
		if (objectsToHide != null)
		{
			for (int index = 0; index < objectsToHide.Length; index++)
			{
				if (objectsToHide[index] != null)
					objectsToHide[index].SetActive(false);
			}
		}
	}

}


