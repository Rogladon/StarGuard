using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploseMissle : Missle
{
	public Explose explose;

	protected override void OnThisDestroy() {
		base.OnThisDestroy();
		GameObject go = Instantiate(explose.gameObject);
		go.transform.position = transform.position;
		go.tag = tag;
		go.GetComponent<Explose>().Init(damage);
	}
}
