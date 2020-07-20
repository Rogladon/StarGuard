using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Buff {
	public GameObject shieldPrefab;
	private GameObject go;
	public int countHit;
	float alpha;
	int curHits;
	public Material material;
	protected override void Action() {
		if (!constains) {
			alpha = material.color.a;
		} else {
			entity.shield++;
			entity.RemoveBuff(this);
			return;
		}
		go = Instantiate(shieldPrefab,entity.transform);
		material = Instantiate(material);
		go.GetComponent<Renderer>().material = material;
		go.transform.localPosition = Vector3.zero;
		
		//speed = ((part.main.startColor.color.a - minColorAlpha)/timeLife);
		entity.shield = countHit;
		curHits = countHit;
	}

	protected override void ActionUpdate() {
		if(curHits != entity.shield) {
			_time -= (timeLife / countHit) * (curHits - entity.shield);
			curHits = entity.shield;
		}
		Color c = material.color;
		c.a = (alpha / timeLife) * (timeLife- _time);
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
