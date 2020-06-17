using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
	public Entity entity;
	public float avengerTime;
	public float _timer;
	MainGeneration.AreaCreate area;

	public void Init(Entity e, float t, MainGeneration.AreaCreate a, float _k) {
		entity = e;
		avengerTime = t;
		area = a;
		k = _k;
		time = k / avengerTime;
		timeSpeed = k / entity.speed;
	}
	public float time = 0;
	public float timeSpeed = 0;
	public float k;
	private void Update() {
		avengerTime = k / time;
		_timer-=Time.deltaTime;
		time += Time.deltaTime;
		if(_timer <= 0) {
			GameObject go = Instantiate(entity.gameObject);
			go.transform.position = area.GetPosition();
			go.transform.position = area.GetPosition();
			go.transform.up = area.GetDirect();
			go.GetComponent<Entity>().speed = k/timeSpeed;
			_timer = Random.Range(avengerTime - avengerTime / 4, avengerTime + avengerTime / 4);
		}
	}
}
