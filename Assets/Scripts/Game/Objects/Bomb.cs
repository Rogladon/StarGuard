using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Missle
{
	public Explose explose;
	public Light light;
	public float timeLife;
	float timer;
	float speed;
	private void Start() {
		speed = (light.color.g) / timeLife;
	}
	private void Update() {
		timer += Time.deltaTime;
		Color c = light.color;
		c.g -= speed*Time.deltaTime;
		c.b -= speed * Time.deltaTime;
		light.color = c;
		if(timer >= timeLife) {
			Destroy(gameObject);
			OnThisDestroy();
		}
		Move();
	}

	protected override void OnThisDestroy() {
		base.OnThisDestroy();
		GameObject go = Instantiate(explose.gameObject);
		go.transform.position = transform.position;
		go.tag = tag;
		go.GetComponent<Explose>().Init(damage);
	}
}
