using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatSystem {
	[SerializeField]
	private BarControl bar;
	[SerializeField]
	private float maxValue_;	// max value of this stat
	[SerializeField]
	private float currentValue_; // current value of this stat

	public float CurrentValue {
		get{ return currentValue_; }
		set{
			this.currentValue_ = Mathf.Clamp(value, 0, MaxValue);
			bar.Value = currentValue_;
		}
	}

	public float MaxValue{
		get { return maxValue_; }
		set{
			this.maxValue_ = value;
			bar.MaxValue = maxValue_;
		}
	}

	public void Initialize(){
		this.MaxValue = maxValue_;
		this.CurrentValue = currentValue_;
	}
}