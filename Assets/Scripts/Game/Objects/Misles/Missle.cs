using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : MonoBehaviour
{
	public float speed;
	public int damage;
	public Vector2 dir;


	public void SetInfo(float speed, int damage, Vector2 dir) {
		this.speed = speed;
		this.damage = damage;
		this.dir = dir;
		transform.rotation = Quaternion.Euler(dir);
	}

	void Update() {
		
		transform.position += transform.up * speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag(tag)) {
			Entity entity;
			if(other.TryGetComponent(out entity)) {
				entity.DoHit(damage);
				Destroy(gameObject);
			}
		}
	}

	private void OnDestroy() {
		
	}
}
