using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	Entity entity;
	public float startSpeed;
	public float maxSpeed;
	public float kxSpeed;
	private float speed;
	private void Start() {
		speed = startSpeed;
		entity = PlayManager.entityPlayer;
	}

	private void Update() {
		if (!entity) return;
		Vector3 dir = (entity.position - (Vector2)transform.position).normalized;
		transform.position += dir * speed *Time.deltaTime;
		if(speed < maxSpeed) {
			speed += kxSpeed;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			entity.coins++;
			Destroy(gameObject);
		}
	}
}
