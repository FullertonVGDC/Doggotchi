using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour {
	public Button myButton;
	public Texture2D newPointer;
	public CursorMode cursorMode = CursorMode.Auto;
	public Vector2 hotspot = Vector2.zero;


	// Use this for initialization
	void Start () {
		myButton.onClick.AddListener(TaskOnClick);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void TaskOnClick(){
			Cursor.SetCursor(newPointer, hotspot, cursorMode);
			IsWeapon.weapon = gameObject.tag;
	}
}
