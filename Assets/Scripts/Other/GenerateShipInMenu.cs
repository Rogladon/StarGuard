using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShipInMenu : MonoBehaviour
{
	public SimpleFly fly;
	public RootRandom root;
	public Material mat;
	public float avengerTime;
	public float avengerSpeed;
	float _time;

	public void Start() {
		_time = RandomTime(avengerTime);
	}

	public void Update() {
		avengerTime = 5-GameManager.openSkins.Count/2;
		_time -= Time.deltaTime;
		if(_time <= 0) {
			_time = RandomTime(avengerTime);
			int id = GameManager.openSkins[Random.Range(0, GameManager.openSkins.Count)];
			Skin skin = new Skin();
			foreach(var i in GameManager.skins[GameManager.player.pack]) {
				if(i.globalID == id) {
					skin = i;
				}
			}
			GameObject go = Instantiate(skin.prefab);
			go.GetComponent<PlayerController>().enabled = false;
			go.GetComponent<Entity>().speed = RandomTime(avengerSpeed);
			go.GetComponent<PolygonCollider2D>().isTrigger = true;
			go.GetComponentInChildren<SpriteRenderer>().sortingOrder = -6;
			foreach ( var i in go.GetComponentsInChildren<ParticleSystem>()) {
				i.GetComponent<Renderer>().material = mat;
			}
			go.transform.GetComponentInChildren<Weapon>().enabled = false;
			AI ai = go.AddComponent<AI>();
			ai.actions.Add(fly);
			ai.actions.Add(root);
			MainGeneration.AreaCreate.Area area;
			area = (MainGeneration.AreaCreate.Area)Random.Range(0,3);
			if (!ai.randomValue.ContainsKey("DefaultDir")) {
				ai.randomValue.Add("DefaultDir", 0);
			}
			switch (area) {
				case MainGeneration.AreaCreate.Area.left:
					ai.randomValue["DefaultDir"] = 90;
					break;
				case MainGeneration.AreaCreate.Area.right:
					ai.randomValue["DefaultDir"] = -90;
					break;
				case MainGeneration.AreaCreate.Area.up:
					ai.randomValue["DefaultDir"] = 0;
					break;
			}
			go.transform.position = MainGeneration.AreaCreate.GetPosition(area);
			float scale = Random.Range(0.7f, 1);
			go.transform.localScale *= scale;
		}
	}

	public float RandomTime(float aTime) {
		return Random.Range(aTime - aTime / 3, aTime + aTime / 3);
	}
}
