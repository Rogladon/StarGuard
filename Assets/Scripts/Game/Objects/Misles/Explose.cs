using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explose : MonoBehaviour
{
	public float radius;
	public float factor;
	public float speed;
	public float speedPart;
	public int damage;
	public CircleCollider2D circleCollider;

	public void Start() {
		circleCollider = GetComponent<CircleCollider2D>();
		
		GetComponent<ParticleSystem>().startLifetime = (radius - circleCollider.radius) / speed;
		GetComponent<ParticleSystem>().startSpeed = speed;
	}
	public void Init(int d) {
		damage = (int)Mathf.Round(d * factor);
	}

	public void Update() {
		if (circleCollider.radius >= radius) Destroy(gameObject);
		circleCollider.radius += speed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag(tag)) {
			Entity entity;
			if (other.TryGetComponent(out entity)) {
				entity.DoHit(damage);
			}
		}
	}
}
