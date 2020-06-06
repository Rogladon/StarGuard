using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
	public GameObjectFloatDictionary Backgraunds= GameObjectFloatDictionary.New<GameObjectFloatDictionary>();
	public Dictionary<string, float> backgrounds { get { return Backgraunds.dictionary; } }

	public List<GameObject> _back;
	private float up;
	private List<List<GameObject>> back = new List<List<GameObject>>();

	private void Start() {
		up = Camera.main.ViewportToWorldPoint(new Vector3(0, 1)).y;
		for(int j = 0; j < _back.Count; j++) {
			back.Add(new List<GameObject>());
			back[j].Add(_back[j]);
		}
		int i = 0;
		foreach(var s in backgrounds) {
			back[i][0].tag = s.Key;
			i++;
		}
	}

	void Update()
    {
       foreach(var l in back) {
			float size = l[0].GetComponent<BoxCollider2D>().size.y;
			if(l[l.Count-1].transform.position.y+size/2 < up + 10) {
				GameObject go = Instantiate(l[l.Count - 1], transform);
				go.transform.position = new Vector3(0, l[l.Count - 1].transform.position.y + size,0);
				l.Add(go);
			}
			foreach (var i in l) {
				i.transform.position -= i.transform.up * backgrounds[i.tag]*Time.deltaTime;
			}
		}
    }
}
