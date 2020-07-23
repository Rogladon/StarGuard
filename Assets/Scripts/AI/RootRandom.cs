using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootRandom: Action
{
	public Vector2 minMaxDir;
	public float speedRoot;
	string dir = "dir";
	string id = "RootRandom";
	string defaultDir = "DefaultDir";

	public override void StartAction(AI ai) {
		if (!ai.randomValue.ContainsKey(defaultDir)) {
			ai.randomValue.Add(defaultDir, 0);
			foreach (var i in ai.actions) {
				if (i.GetType() == typeof(SimpleFly)) {
					ai.randomValue[defaultDir] = ((SimpleFly)i).dir;
				}
			}
		}
		ai.randomValue[dir] = ai.randomValue[defaultDir];
		
		ai.randomValue.Add(id, 0);
		ai.randomValue[id] = ai.randomValue[defaultDir] + Random.Range(minMaxDir.x, minMaxDir.y);
	}

	public override void UpdateAction(AI ai) {
		ai.randomValue[dir] += (Mathf.Sign(ai.randomValue[id])) *speedRoot * Time.deltaTime;
		float rad = (ai.randomValue[dir] - 90) * Mathf.PI / 180;
		ai.entity.directon = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
		if(Mathf.Abs(ai.randomValue[dir]) > Mathf.Abs(ai.randomValue[id])) {
			ai.randomValue[id] = ai.randomValue[defaultDir] + Random.Range(minMaxDir.x, minMaxDir.y);
		}

	}
}
