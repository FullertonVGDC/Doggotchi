using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarControl : MonoBehaviour {
	private float fillAmount;
	private float lerpSpeed = 2.5f;
	[SerializeField]
	private Image content;

	[SerializeField]
	private Text valueText;

	public float MaxValue { get; set; }
	public float Value{ 
		set{ 
			string[] tmp = valueText.text.Split(':');
			valueText.text = tmp[0] + ": " + value + " / " + MaxValue;
			fillAmount = Map(value, 0, MaxValue, 0, 1); 
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleBar();
	}

	private void HandleBar(){
		if(content.fillAmount != fillAmount){
			content.fillAmount = Mathf.Lerp(content.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
		}
	}

	private float Map(float value_, float inMin_, float inMax_, float outMin_, float outMax_){
		return (value_ - inMin_) * (outMax_ - outMin_) / (inMax_ - inMin_) + outMin_;
	}
}
