using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Buff {
	public GameObject shieldPrefab;
	public GameObject go;
	public ParticleSystem part;
	public float minColorAlpha;
	public float speed;
	protected override void Action() {
		go = Instantiate(shieldPrefab,entity.transform);
		go.transform.localPosition = Vector3.zero;
		part = go.GetComponent<ParticleSystem>();
		speed = ((part.main.startColor.color.a - minColorAlpha)/timeLife);
	}

	protected override void ActionUpdate() {
		Color c = part.main.startColor.color;
		c.a -= speed * Time.fixedDeltaTime;
		part.startColor = c;
	}

	protected override void OnDestroy() {
		Destroy(go);
	}
}
