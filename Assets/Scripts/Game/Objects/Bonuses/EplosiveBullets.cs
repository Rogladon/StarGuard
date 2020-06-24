using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EplosiveBullets : Buff {
	public GameObject typeMissle;
	List<Weapon> weapons;
	protected override void Action() {
		weapons = ListUtilits.CopyTo(entity.weapons);
		foreach (var i in weapons) {
			i.prefabMissle = typeMissle;
		}
		foreach (var i in entity.staticWeapons) {
			i.prefabMissle = typeMissle;
		}
		
	}

	protected override void ActionUpdate() {
		if (weapons.Count != entity.weapons.Count) {
			foreach (var i in entity.weapons) {
				if (!weapons.Contains(i)) {
					i.prefabMissle = typeMissle;
				}
			}
			weapons = ListUtilits.CopyTo(entity.weapons);
		}
	}

	protected override void OnDestroy() {
		if (!constains) {
			foreach (var i in entity.weapons) {
				i.prefabMissle = entity.missles[entity.defaulTypeMIssle];
			}
			foreach (var i in entity.staticWeapons) {
				i.prefabMissle = entity.missles[entity.defaulTypeMIssle];
			}
		}
	}
}
