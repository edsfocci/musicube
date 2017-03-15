using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputAudioManager : MonoBehaviour {
	public AudioSource leftAudio;
	public AudioSource rightAudio;
	public AudioSource frontAudio;
	public AudioSource backAudio;
	public AudioSource topAudio;
	public AudioSource bottomAudio;

	public BasicTrackableEventHandler inputTarget;

	// Use this for initialization
	void Start () {
		inputTarget.OnTrackingLost += HandleTrackingLost;

		leftAudio.Play();
		rightAudio.Play();
		frontAudio.Play();
		backAudio.Play();
		topAudio.Play();
		bottomAudio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		float rotX = inputTarget.transform.localRotation.eulerAngles.x;
		float rotY = inputTarget.transform.localRotation.eulerAngles.y;
		float rotZ = inputTarget.transform.localRotation.eulerAngles.z;
		//print (rotZ);

		if (rotZ > 135.0F && rotZ < 225.0F) {
			if (rotY > 225.0F && rotY < 315.0F) {
				leftAudio.UnPause ();

				rightAudio.Pause ();
				frontAudio.Pause ();
				backAudio.Pause ();
				topAudio.Pause ();
				bottomAudio.Pause ();
			} else if (rotY > 45.0F && rotY < 135.0F) {
				rightAudio.UnPause ();

				leftAudio.Pause ();
				frontAudio.Pause ();
				backAudio.Pause ();
				topAudio.Pause ();
				bottomAudio.Pause ();
			} else if (rotY > 135.0F && rotY < 225.0F) {
				frontAudio.UnPause ();

				leftAudio.Pause ();
				rightAudio.Pause ();
				backAudio.Pause ();
				topAudio.Pause ();
				bottomAudio.Pause ();
			} else {
				backAudio.UnPause ();

				leftAudio.Pause ();
				rightAudio.Pause ();
				frontAudio.Pause ();
				topAudio.Pause ();
				bottomAudio.Pause ();
			}
		} else if (rotZ > 135.0F && rotZ < 225.0F) {
			topAudio.UnPause ();

			leftAudio.Pause ();
			rightAudio.Pause ();
			frontAudio.Pause ();
			backAudio.Pause ();
			bottomAudio.Pause ();
		} else {
			bottomAudio.UnPause ();

			leftAudio.Pause ();
			rightAudio.Pause ();
			frontAudio.Pause ();
			backAudio.Pause ();
			topAudio.Pause ();
		}
	}

	void HandleTrackingLost()
	{
		leftAudio.Pause();
		rightAudio.Pause();
		frontAudio.Pause();
		backAudio.Pause();
		topAudio.Pause();
		bottomAudio.Pause();
	}
}
