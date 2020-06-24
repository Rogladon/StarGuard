using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Shoot : Action {
	public Vector2 minMaxTime;
	public GameObject prefabMissle;
	string _timer = "_timerShoot";
	string timeShoot = "timerShoot";

	public override void StartAction(AI ai) {
		foreach(var i in ai.entity.staticWeapons) {
			i.prefabMissle = prefabMissle;
		}
		ai.randomValue.Add(_timer, 0);
		ai.randomValue.Add(timeShoot, 1);
	}

	public override void UpdateAction(AI ai) {
		ai.randomValue[_timer] += Time.deltaTime;
		if(ai.randomValue[_timer] > ai.randomValue[timeShoot]) {
			ai.randomValue[_timer] = 0;
			ai.randomValue[timeShoot] = Random.Range(minMaxTime.x, minMaxTime.y);
			foreach(var i in ai.entity.staticWeapons) {
				i.DoShoot();
			}
		}
	}
}
