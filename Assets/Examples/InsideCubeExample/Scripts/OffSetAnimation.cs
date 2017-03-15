using UnityEngine;
using System.Collections;

public class OffSetAnimation : MonoBehaviour 
{
	Vector2 offSetSpeed = new Vector2(0f,1.5f);
	Vector2 offSetValue = Vector2.zero;

	bool isAnimating = false;

	void Start () 
	{
		Invoke ("StartAnimation", 1f);
	}

	void Update () 
	{
		if (isAnimating) 
		{
			offSetValue -= offSetSpeed * Time.deltaTime;
			GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offSetValue);

			if ( Mathf.Abs( offSetValue.y )>= 1f) 
			{
				HandleAnimationComplete ();
			}
		}
	}

	void HandleAnimationComplete()
	{
		isAnimating = false;
		offSetValue = Vector2.one;
		GetComponent<Renderer> ().material.SetTextureOffset ("_MainTex", offSetValue);
		Invoke ("StartAnimation", Random.Range(1f,3f));
	}

	void StartAnimation()
	{
		isAnimating = true;
	}


}
