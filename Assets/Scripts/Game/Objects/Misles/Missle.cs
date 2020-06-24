using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
	protected float speed;
	protected int damage;
	protected Vector2 dir;
	public Weapon.TypeMissle typeMissle;

	public void SetInfo(float speed, int damage, Vector2 dir) {
		this.speed = speed;
		this.damage = damage;
		this.dir = dir;
		transform.up = dir;
	}

	void Update() {
		transform.position += transform.up * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag(tag)) {
			Entity entity;
			if(other.TryGetComponent(out entity)) {
				entity.DoHit(damage);
				OnThisDestroy();
				Destroy(gameObject);
			}
		}
	}

	virtual protected void OnThisDestroy() {

	}
}
