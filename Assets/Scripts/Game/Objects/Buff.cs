﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Buff : MonoBehaviour {
	public string nameBuff;
	public float timeLife;
	protected Entity entity;

	protected float _time = 0.1f;

	void Awake() {
		entity = GetComponentInParent<Entity>();
		Action();
	}

	// Update is called once per frame
	void Update() {
		_time += Time.deltaTime;
		if (_time >= timeLife) {
			entity.RemoveBuff(this);
		}
		ActionUpdate();
	}

	virtual protected void ActionUpdate() {

	}

	abstract protected void Action();

	abstract protected void OnDestroy();
}
