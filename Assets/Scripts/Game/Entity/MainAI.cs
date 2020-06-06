using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRun : State {

	public SimpleRun(AI _ai, Entity _entity) {
		ai = _ai;
		entity = _entity;

	}
	public override void StartState() {

	}
	public override void Behaviour() {
		entity.position -= (Vector2)entity.transform.up * entity.speed * Time.deltaTime;
	}

	public override bool Condition() {
		if (true) {
			return true;
		}
		return false;
	}

	public override void Init() {
		PrevInit();
		priority = 0;
		nameState = "SimpleRun";
	}
}