using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class AI : MonoBehaviour
{
	public enum sta {
		SimpleFly,
		Shoot,
		RootRandom,
		HorizontMove,
		PutBomb
	}
	[HideInInspector]
	public List<Action> actions = new List<Action>();
	public Entity entity;
	[HideInInspector]
	public sta _enum;
	public Dictionary<string, float> randomValue = new Dictionary<string, float>();
    void Start()
    {
		entity = GetComponent<Entity>();
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
			i.UpdateAction(this);
		}
	}

	void StartActions() {
		foreach(var i in actions) {
			i.StartAction(this);
		}
	}
}
