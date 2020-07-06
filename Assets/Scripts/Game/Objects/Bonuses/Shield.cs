using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Buff {
	public GameObject shieldPrefab;
	public GameObject go;
	public ParticleSystem part;
	public float minColorAlpha;
	public int countHit;
	float alpha;
	public Material material;
	protected override void Action() {
		go = Instantiate(shieldPrefab,entity.transform);
		go.transform.localPosition = Vector3.zero;
		part = go.GetComponent<ParticleSystem>();
		if (!constains) {
			alpha = material.color.a;
			Debug.Log(alpha);
		} else {
			entity.RemoveBuff(this);
		}
		//speed = ((part.main.startColor.color.a - minColorAlpha)/timeLife);
		entity.shield = countHit;
	}

	protected override void ActionUpdate() {
		Color c = material.color;
		c.a = (alpha / (float)countHit) * (float)entity.shield;
		Debug.Log(entity.shield + " "+c.a);
		material.color = c;
		if(entity.shield == 0) {
			entity.RemoveBuff(this);
		}
	}

	protected override void OnDestroy() {
		if (alpha != 0) {
			Color c = material.color;
			c.a = alpha;
			material.color = c;
		}
		Destroy(go);
	}
}
