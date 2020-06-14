using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Action : ScriptableObject {
	public Entity entity;
	public string type {
		get {
			return this.GetType().Name;
		}
	}

	public virtual void Start() { }

	public virtual void Update() { }

}