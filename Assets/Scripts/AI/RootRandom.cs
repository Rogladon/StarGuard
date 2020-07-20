using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootRandom: Action
{
	public Vector2 minMaxDir;
	public float speedRoot;
	string dir = "dir";
	string id = "RootRandom";

	public override void StartAction(AI ai) {
		ai.randomValue.Add(dir, 0);
		ai.randomValue.Add(id, 0);
		ai.randomValue[id] = Random.Range(minMaxDir.x, minMaxDir.y);
	}

	public override void UpdateAction(AI ai) {
		ai.randomValue[dir] += (Mathf.Sign(ai.randomValue[id])) *speedRoot * Time.deltaTime;
		float rad = (ai.randomValue[dir] - 90) * Mathf.PI / 180;
		ai.entity.directon = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
		if(Mathf.Abs(ai.randomValue[dir]) > Mathf.Abs(ai.randomValue[id])) {
			ai.randomValue[id] = Random.Range(minMaxDir.x, minMaxDir.y);
		}

	}
}
