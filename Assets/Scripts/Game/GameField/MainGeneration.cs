﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGeneration : MonoBehaviour {
	public EntityTimeDictionary Enemies = EntityTimeDictionary.New<EntityTimeDictionary>();
	public Dictionary<Entity, float> enemies = new Dictionary<Entity, float>();
	public Dictionary<Entity, float> speedEnemies = new Dictionary<Entity, float>();
	public float k;
	public float kSpeed;
	public float avengerTime;
	public class AreaCreate {
		public enum Area {
			left,
			right,
			up
		}
		public Area area;
		public AreaCreate(Area area) {
			this.area = area;
		}
		public AreaCreate(int i) {
			area = Area.up;
		}
		#region GETPOSITION
		public static Vector3 GetPosition(Area area) {
			Vector3 pos = new Vector3();
			Camera cam = Camera.main;
			float up = cam.ViewportToWorldPoint(new Vector3(0, 1)).y;
			float down = cam.ViewportToWorldPoint(new Vector3(0, 0)).y;
			float right = cam.ViewportToWorldPoint(new Vector3(1, 0)).x + PlayManager.borderField;
			float left = cam.ViewportToWorldPoint(new Vector3(0, 1)).x - PlayManager.borderField;
			switch (area) {
				case Area.left:
					pos.y = Random.Range(up, down);
					pos.x = left - 3;
					pos.z = 0;
					break;
				case Area.right:
					pos.y = Random.Range(up, down);
					pos.x = right + 3;
					pos.z = 0;
					break;
				case Area.up:
					pos.y = up + 0.5f;
					pos.x = Random.Range(left, right);
					pos.z = 0;
					break;
			}
			return pos;
		}
		public Vector3 GetPosition() {
			Vector3 pos = new Vector3();
			Camera cam = Camera.main;
			float up = cam.ViewportToWorldPoint(new Vector3(0, 1)).y;
			float down = cam.ViewportToWorldPoint(new Vector3(0, 0)).y;
			float right = cam.ViewportToWorldPoint(new Vector3(1, 0)).x;
			float left = cam.ViewportToWorldPoint(new Vector3(0, 1)).x;
			switch (area) {
				case Area.left:
					pos.y = Random.Range(up, down);
					pos.x = left - 3;
					pos.z = 0;
					break;
				case Area.right:
					pos.y = Random.Range(up, down);
					pos.x = right + 3;
					pos.z = 0;
					break;
				case Area.up:
					pos.y = up + 0.5f;
					pos.x = Random.Range(left, right);
					pos.z = 0;
					break;
			}
			return pos;
		}
		#endregion
		//Доделать направление под углом
		#region GETDIRECT 
		public static Vector3 GetDirect(Area area) {
			Vector3 direct = new Vector3();
			switch (area) {
				case Area.left:
					direct = Vector3.right;
					break;
				case Area.right:
					direct = Vector3.left;
					break;
				case Area.up:
					direct = Vector3.down;
					break;
			}
			return direct;
		}

		public Vector3 GetDirect() {
			Vector3 direct = new Vector3();
			switch (area) {
				case Area.left:
					direct = Vector3.right;
					break;
				case Area.right:
					direct = Vector3.left;
					break;
				case Area.up:
					direct = Vector3.down;
					break;
			}
			return direct;
		}
		#endregion

	}
	[SerializeField]
	public AreaCreate area;

	Dictionary<Entity, float> entitySpeed = new Dictionary<Entity, float>();

	private void Start() {
		area = new AreaCreate(AreaCreate.Area.up);
		foreach(var i in enemies) {
			i.Key.speed = speedEnemies[i.Key];
			Debug.Log(i.Key.speed);
			entitySpeed.Add(i.Key, kSpeed / i.Key.speed);
		}
		//foreach(var i in enemies) {
		//	GameObject go = new GameObject();
		//	Generate g = go.AddComponent<Generate>();
		//	g.Init(i.Key, i.Value*enemies.Count, areaCreate,k);

		//	Debug.Log(g.avengerTime);
		//}
		time = k / avengerTime;
	}
	public float time = 0;
	public float timeSpeed = 0;
	public float _timer;
	private void Update() {
		Enemies.dictionary = enemies;
		foreach(var i in enemies) {
			entitySpeed[i.Key] += Time.deltaTime;
		}
		avengerTime = k / time;
		_timer -= Time.deltaTime;
		time += Time.deltaTime;
		if (_timer <= 0) {
			Entity entity = enemies.Keys.GetEnumerator().Current;
			int minPrecent = int.MaxValue;
			Dictionary<Entity, int> keyValues = new Dictionary<Entity, int>();
			foreach (var i in enemies) {
				int p = Random.Range(0, Mathf.RoundToInt(100 / i.Value));
				keyValues.Add(i.Key, p);
				if (p < minPrecent) {
					entity = i.Key;
					minPrecent = p;
				}
			}
			GameObject go = Instantiate(entity.gameObject);
			go.transform.position = area.GetPosition();
			go.transform.up = area.GetDirect();
			go.GetComponent<Entity>().speed = (entity.speed*2)-(kSpeed / entitySpeed[entity]);
			_timer = Random.Range(avengerTime - avengerTime / 4, avengerTime + avengerTime / 4);
		}
	}


}