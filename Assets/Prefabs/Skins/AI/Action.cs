using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Action : ScriptableObject {
	public string type {
		get {
			return this.GetType().Name;
		}
	}

	public virtual void StartAction(AI ai) { }

	public virtual void UpdateAction(AI ai) { }

}