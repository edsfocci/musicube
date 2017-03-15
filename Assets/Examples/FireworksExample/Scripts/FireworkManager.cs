using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class FireworkManager : MonoBehaviour 
{
	public Transform headTransform;
	public BasicTrackableEventHandler imageTarget;
	public ParticleSystem fireworkEmitter;
	public AudioClip fireworkSFX;

	private AudioSource audSource;

	void Start()
	{
		audSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		if ( Input.GetMouseButtonDown(0) && imageTarget.isTracking )
		{
			ShootFirework();
		}
	}

	void LateUpdate()
	{
		//The firework emitter needs to have the same position and rotation as the HoloCube so that it will look like the fireworks are coming out of the box
		//However, we can't parent the fireworks to the cube because we want the fireworks to still be visibile even when tracking is lost. So we will do a "soft"
		//parenting by just manually setting it's position and rotation.

		fireworkEmitter.transform.parent.position = imageTarget.transform.position;
		fireworkEmitter.transform.parent.rotation = imageTarget.transform.rotation;
	}

	public void ShootFirework()
	{
		fireworkEmitter.Emit(1);

		if (fireworkSFX != null)
		{
			audSource.PlayOneShot(fireworkSFX);
		}
	}
}
