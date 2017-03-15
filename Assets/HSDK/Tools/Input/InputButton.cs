using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class InputButton : MonoBehaviour {
	enum ButtonStateEnum{
		up,hover,down
	}
	public Sprite btnUpSprite;
	public Sprite btnHoverSprite;
	public Sprite btnDownSprite;

	public UnityEvent onClick;
	public UnityEvent onHoverStart;
	public UnityEvent onHoverEnd;

	SpriteRenderer btnTexture;
	AudioSource player;
	bool isHoverOver = false;
	ButtonStateEnum btnState = ButtonStateEnum.up;

	void Start () {
		if (GetComponent<SpriteRenderer> () != null) {
			btnTexture = GetComponent<SpriteRenderer> ();
		}
		if (GetComponent<AudioSource> () != null) {
			player = GetComponent<AudioSource> ();
		}
	}

	void OnClick()
	{
		onClick.Invoke ();
		if (player != null) {
			player.Play ();
		}
		btnState = ButtonStateEnum.down;
		UpdateState ();
		CancelInvoke ();
		Invoke ("UnClick", .2f);

	}
	void UnClick(){
		if (isHoverOver) {			
			btnState = ButtonStateEnum.hover;
			UpdateState ();
		}
	}

	void OnHoverStart()
	{		
		if (!isHoverOver) 
		{			
			isHoverOver = true;
			onHoverStart.Invoke ();
			btnState = ButtonStateEnum.hover;
			UpdateState ();
		}
	}
	void OnHoverEnd()
	{
		isHoverOver = false;
		onHoverEnd.Invoke ();
		btnState = ButtonStateEnum.up;
		UpdateState ();
	}

	void UpdateState(){
		if (btnTexture != null) {
			switch (btnState) {
			case ButtonStateEnum.up:
				if (btnUpSprite != null) {
					btnTexture.sprite = btnUpSprite;
				}
				break;
			case ButtonStateEnum.hover:
				if (btnHoverSprite != null) {
					btnTexture.sprite = btnHoverSprite;
				}
				break;
			case ButtonStateEnum.down:
				if (btnDownSprite != null) {
					btnTexture.sprite = btnDownSprite;
				}
				break;
			}
		}
	}
}
