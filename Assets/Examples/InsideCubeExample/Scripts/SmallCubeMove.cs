using UnityEngine;
using System.Collections;

public class SmallCubeMove : MonoBehaviour 
{
	bool rotateAroundX = true;
	bool rotateAroundY = true;
	bool rotateAroundZ = true;

	public float rotateXSpeed = 1f;
	public float rotateYSpeed = -1f;
	public float rotateZSpeed = 0f;

	public float rotateSpeed = 37f;

	void Update () 
	{
		RotateChange ();
		transform.RotateAround (transform.position,new Vector3(rotateXSpeed, rotateYSpeed, rotateZSpeed), rotateSpeed * Time.deltaTime);
	}
		
	void RotateChange()
	{
		if (rotateAroundX)
		{
			rotateXSpeed += 0.5f*Time.deltaTime;

			if (rotateXSpeed >= 1f) 
			{
				rotateXSpeed = 1f;
				rotateAroundX = false;
			}
		} 
		else 
		{
			rotateXSpeed -= 0.5f*Time.deltaTime;

			if (rotateXSpeed <= -1f) 
			{
				rotateXSpeed = -1f;
				rotateAroundX = true;
			}
		}

		if (rotateAroundY) 
		{
			rotateYSpeed += 0.5f*Time.deltaTime;

			if (rotateYSpeed >= 1f) 
			{
				rotateYSpeed = 1f;
				rotateAroundY = false;
			}
		} 
		else 
		{
			rotateYSpeed -= 0.5f*Time.deltaTime;

			if (rotateYSpeed <= -1f) 
			{
				rotateYSpeed = -1f;
				rotateAroundY = true;
			}
		}

		if (rotateAroundZ) 
		{
			rotateZSpeed += 0.5f*Time.deltaTime;
			if (rotateZSpeed >= 1f) 
			{
				rotateZSpeed = 1f;
				rotateAroundZ = false;
			}
		} 
		else 
		{
			rotateZSpeed -= 0.5f*Time.deltaTime;

			if (rotateZSpeed <= -1f) 
			{
				rotateZSpeed = -1f;
				rotateAroundZ = true;
			}
		}
	}

}
