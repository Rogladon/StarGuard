using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBomb : Action {
	public Vector2 minMaxTime;
	public Missle prefabMissle;
	string _timer = "_timerShoot";
	string timeShoot = "timerShoot";

	public override void StartAction(AI ai) {
		minMaxTime.x = ((-PlayManager.globalVerticalField/4)) / ai.entity.speed;
		minMaxTime.y = ((-PlayManager.globalVerticalField)-1) / ai.entity.speed;
		foreach (var i in ai.entity.staticWeapons) {
			i.speed = PlayManager.speed;
			i.prefabMissle = prefabMissle;
		}
		ai.randomValue.Add(_timer, 0);
		ai.randomValue.Add(timeShoot, Random.Range(minMaxTime.x, minMaxTime.y));
	}

	public override void UpdateAction(AI ai) {
		ai.randomValue[_timer] += Time.deltaTime;
		if (ai.randomValue[_timer] > ai.randomValue[timeShoot]) {
			ai.randomValue[_timer] = 0;
			ai.randomValue[timeShoot] = Random.Range(minMaxTime.x, minMaxTime.y);
			foreach (var i in ai.entity.staticWeapons) {
				i.DoShoot();
			}
		}
	}
}
