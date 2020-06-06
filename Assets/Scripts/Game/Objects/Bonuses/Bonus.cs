using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour {

	private void Update() {
		transform.position -= transform.up * PlayManager.speed * Time.deltaTime;
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Player")) {
			Entity entity;
			if(other.TryGetComponent(out entity)) {
				entity.AddBonus(this);
				Debug.Log("Bonus");
			}
			Destroy(gameObject);
		}
	}
}
