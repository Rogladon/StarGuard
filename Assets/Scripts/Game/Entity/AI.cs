using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AI : MonoBehaviour
{
	public enum sta {
		SimpleFly,
		Shoot
	}
	[HideInInspector]
	public List<Action> actions = new List<Action>();
	Entity entity;
	[HideInInspector]
	public sta _enum;
    void Start()
    {
		entity = GetComponent<Entity>();
		foreach (var i in actions) {
			i.entity = entity;
		}
		StartActions();
    }
	[ContextMenu("ClearStat")]
	public void ClearStat() {
		actions.Clear();
	}

    void Update()
    {
		UpdateActions();
    }

	void UpdateActions() {
		foreach (var i in actions) {
			i.Update();
		}
	}

	void StartActions() {
		foreach(var i in actions) {
			i.Start();
		}
	}
}
