using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedShoot : Buff {
	public float factorSpeed;
	List<Weapon> weapons = new List<Weapon>();
	protected override void Action() {
		if (constains) {
			factorSpeed = 1+ ((factorSpeed-1)/2);
			timeLife /= 2;
		}
		weapons = ListUtilits.CopyTo(entity.weapons);
		foreach(var i in weapons) {
			i.reload /= factorSpeed;
			i.speed *= (factorSpeed/2);
		}
		foreach (var i in entity.staticWeapons) {
			i.reload /= factorSpeed;
			i.speed *= (factorSpeed / 2);
		}
	}

	protected override void ActionUpdate() {
		if(weapons.Count != entity.weapons.Count) {
			foreach(var i in entity.weapons) {
				if (!weapons.Contains(i)) {
					i.reload /= factorSpeed;
					i.speed *= (factorSpeed / 2);
				}
			}
			weapons = ListUtilits.CopyTo(entity.weapons);
		}
	}

	protected override void OnDestroy() {
		foreach (var i in entity.weapons) {
			i.reload *= factorSpeed;
			i.speed /= (factorSpeed/2);
		}
		foreach (var i in entity.staticWeapons) {
			i.reload *= factorSpeed;
			i.speed /= (factorSpeed / 2);
		}
	}
}
