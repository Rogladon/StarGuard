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
	public TypeMissle typeMissle;

	[Header("Components")]
	public Entity entity;
	public GameObject prefabMissle;

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
		prefabMissle = entity.missles[typeMissle];
	}


	private void Update() {
		_timer += Time.deltaTime;
		if(_timer >= reload) {
			DoShoot();
			_timer = 0;
		}
	}

	private void DoShoot() {
		GameObject go = Instantiate(prefabMissle);
		go.transform.position = transform.position;
		go.tag = entity.tag;
		go.GetComponent<Missle>().SetInfo(speed, damage, transform.rotation.eulerAngles);
	}

	public void ResetTimer() {
		_timer = 0;
	}
}
