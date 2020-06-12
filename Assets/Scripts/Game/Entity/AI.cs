using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Stat : ScriptableObject{
	protected Entity entity;
	public float a;
	public string type {
		get {
			return this.GetType().Name;
		}
	}
}
[SerializeField]
public class SimpleFly : Stat {
	public float speed;
	public float dir;
	//public SimpleFly() {
		
	//}
	public void Update() {
		//entity.position += entity.directon * entity.speed * Time.deltaTime;
	}
}
[SerializeField]
public class Shoot : Stat {
	public Vector2 minMaxTime;
	public GameObject prefabMissle;
	//public Shoot(Entity e) {
	//	entity = e;
	//}
	public void Update() {
		
	}
}


public class AI : MonoBehaviour
{
	public string Name;
	public enum sta {
		SimpleFly,
		Shoot
	}
	[HideInInspector]
	public List<Stat> stats = new List<Stat>();
	Entity entity;
	[HideInInspector]
	public sta _enum;
    void Start()
    {
		entity = GetComponent<Entity>();
    }
	[ContextMenu("ClearStat")]
	public void ClearStat() {
		stats.Clear();
	}

    void Update()
    {
		
    }
}
