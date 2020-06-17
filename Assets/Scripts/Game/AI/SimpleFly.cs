using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SimpleFly : Action {
	public float dir;
	public override void StartAction(AI ai) {
		float rad = (dir - 90) * Mathf.PI / 180;
		ai.entity.directon = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
	}
	public override void UpdateAction(AI ai) {
		ai.entity.position += ai.entity.directon * ai.entity.speed * Time.deltaTime;
	}
}