using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontMove : Action {
	public Vector2 distance;
	public Vector2 timeMove;
	public float speedMove;
	string dist = "hMHorizonDistance";
	string time = "hMTimeMove";
	string _timer = "hMtime";

	public override void StartAction(AI ai) {
		ai.randomValue.Add(time, Random.Range(timeMove.x,timeMove.y));
		ai.randomValue.Add(dist,0);
		RandomDist(ai);
		ai.randomValue.Add(_timer, 0);
	}

	public override void UpdateAction(AI ai) {
		ai.randomValue[_timer] += Time.deltaTime;
		if (ai.randomValue[_timer] > ai.randomValue[time]) {
			if (Mathf.Abs(ai.entity.position.x - ai.randomValue[dist]) > 0.2f) {
				ai.entity.position = Vector2.Lerp(ai.entity.position, new Vector2(ai.randomValue[dist], ai.entity.position.y), speedMove);
				//ai.entity.position += Vector2.right * Mathf.Sign(ai.entity.position.x - ai.randomValue[dist]) * speedMove * Time.deltaTime;
			} else {
				ai.randomValue[_timer] = 0;
				ai.randomValue[time] = Random.Range(timeMove.x, timeMove.y);
				RandomDist(ai);
			}
		}
	}

	void RandomDist(AI ai) {
		do {
			ai.randomValue[dist] = ai.transform.position.x + (Random.Range(distance.x, distance.y)*Mathf.Sign(Random.Range(-1,1)));
		} while (ai.randomValue[dist] < -PlayManager.globalBorderField || ai.randomValue[dist] > PlayManager.globalBorderField);
	}
}
