using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DogObject : MonoBehaviour  {
	//public GameObject dogObj;
	private bool didClick;
	private string currentWeapon;
	[SerializeField]
	private StatSystem hygeine;

	private void Awake(){
		hygeine.Initialize();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//currentWeapon = IsWeapon.weapon;
		if(Input.GetKeyDown(KeyCode.Q)){
			hygeine.CurrentValue -= 10;
		}
		if(Input.GetKeyDown(KeyCode.W)){
			hygeine.CurrentValue += 10;
		}
	}

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0) && currentWeapon == "brush"){
			didClick = true;
		}
	}

	void OnMouseExit(){
		didClick = false;
	}

	public bool DidClick{
		get { return didClick; }
	}

}
