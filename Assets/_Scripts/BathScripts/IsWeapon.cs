using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public static class IsWeapon  {
	public static string weapon;
	public static int timer;

	public static string getWeapon(){
		return weapon;
	}

	public static void AssignWeapon(string name){
		weapon = name;
	}

	public static void ultWeapon(){
		timer = 10;
	}

}
