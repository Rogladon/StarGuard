using System.Collections;
using System.Collections.Generic;
using UnityEngine;
abstract public class Buff : MonoBehaviour {
	public string nameBuff;
	public float timeLife;
	protected Entity entity;
	protected bool constains {
		get {
			foreach (var i in entity.buffs) {
				if (i.GetType() == this.GetType() && i != this) {
					return true;
				}
			}
			return false;
		}
	}
	protected int countBuff {
		get {
			int c = 0;
			foreach (var i in entity.buffs) {
				if (i.GetType() == this.GetType() && i != this) {
					c++;
				}
			}
			return c;
		}
	}

	protected float _time = 0.1f;

	void Awake() {
		entity = GetComponentInParent<Entity>();
		
	}

	private void Start() {
		Action();
	}

	void FixedUpdate() {
		_time += Time.fixedDeltaTime;
		if (_time >= timeLife) {
			entity.RemoveBuff(this);
		}
		ActionUpdate();
	}

	virtual protected void ActionUpdate() {

	}

	abstract protected void Action();

	abstract protected void OnDestroy();
}
