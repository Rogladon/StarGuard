using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EplosiveBullets : Buff {
	public Missle typeMissle;
	public int factorDamage = 1;
	List<Weapon> weapons;
	protected override void Action() {
		if (constains) {
			foreach(var i in entity.buffs) {
				if(i.GetType() == this.GetType()) {
					i.timeLife += timeLife / 2;
					entity.RemoveBuff(this);
				}
			}
		}
		weapons = ListUtilits.CopyTo(entity.weapons);
		foreach (var i in weapons) {
			i.prefabMissle = typeMissle;
			i.damage += factorDamage;
		}
		foreach (var i in entity.staticWeapons) {
			i.prefabMissle = typeMissle;
			i.damage += factorDamage;
		}
		
	}

	protected override void ActionUpdate() {
		if (weapons.Count != entity.weapons.Count) {
			foreach (var i in entity.weapons) {
				if (!weapons.Contains(i)) {
					i.prefabMissle = typeMissle;
					i.damage += factorDamage;
				}
			}
			weapons = ListUtilits.CopyTo(entity.weapons);
		}
	}

	protected override void OnDestroy() {
		if (!constains) {
			foreach (var i in entity.weapons) {
				i.damage -= factorDamage;
				i.prefabMissle = entity.missles[entity.defaulTypeMIssle];
			}
			foreach (var i in entity.staticWeapons) {
				i.damage -= factorDamage;
				i.prefabMissle = entity.missles[entity.defaulTypeMIssle];
			}
		}
	}
}
