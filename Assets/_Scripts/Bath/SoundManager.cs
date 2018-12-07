using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	public AudioSource destroyNoise;
	public AudioSource barkNoise;

	public void PlayNoise(){
		destroyNoise.Play();
	}

	public void Bark(){
		barkNoise.Play();
	}
}
