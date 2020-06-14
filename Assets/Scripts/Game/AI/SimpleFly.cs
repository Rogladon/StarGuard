using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class SimpleFly : Action {
	public float speed;
	public float dir;
	public override void Start() {
		float rad = (dir - 90) * Mathf.PI / 180;
		entity.directon = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
	}
	public override void Update() {
		entity.position += entity.directon * speed * Time.deltaTime;
	}
}