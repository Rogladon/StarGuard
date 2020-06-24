using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	public enum TypeMissle {
		forward,
		explose
	}
	[Header("Stats")]
	public int damage;
	public float reload;
	public float speed;

	[Header("Components")]
	public Entity entity;
	public Missle prefabMissle;

	public Vector2 position {
		get {
			return (Vector2)transform.position;
		}
		set {
			transform.position = (Vector3)value;
		}
	}
	private float _timer;

	public void Awake() {
		entity = GetComponentInParent<Entity>();
		reload *= entity.kReload;
		if (!prefabMissle) {
			if (entity.missles.ContainsKey(entity.defaulTypeMIssle)) {
				prefabMissle = entity.missles[entity.defaulTypeMIssle];
			} 
		}
	}


	private void Update() {
		if (reload == 0) return;
		_timer += Time.deltaTime;
		if(_timer >= reload) {
			DoShoot();
			_timer = 0;
		}
	}

	public void DoShoot() {
		if (entity.player) {
			AudioManager.events.playerShoot.Invoke((int)prefabMissle.typeMissle);
		}
		GameObject go = Instantiate(prefabMissle.gameObject, transform.position, transform.rotation);
		go.transform.position = transform.position;
		go.tag = entity.tag;
		go.GetComponent<Missle>().SetInfo(speed, damage, entity.directon);
	}

	public void ResetTimer() {
		_timer = 0;
	}
}
