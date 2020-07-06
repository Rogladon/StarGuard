using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionValue {
	private string name;
	public ActionValue(AI ai, string name) {
		this.name = name;
		ai.randomValue.Add(name, 0);
	}

	public float Get(AI ai) {
			return ai.randomValue[name];
	}
	public void Set(float f, AI ai) {
			ai.randomValue[name] = f;
	}
}
