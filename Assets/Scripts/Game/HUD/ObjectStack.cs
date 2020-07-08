using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ObjectStack : MonoBehaviour
{
	public enum Type{
		size,
		alphaPlus,
		alphaMinus
	}
	public Type type;
	public List<GameObject> objects;
	public float timeRepeat;
	public float speed;
	public bool start;
	float _time;
	int id = 0;
	float deltaTime = 0.016f;

	public EventTrigger.TriggerEvent @event;
	public void OnEnable() {
		start = true;
		id = 0;
		_time = 0;
		StartCoroutine(UpdateAction());
		//InvokeRepeating("UpdateAction", 0, deltaTime);
	}

	private IEnumerator UpdateAction() {
		if (start) {
			_time += deltaTime;
			switch (type) {
				case Type.size:
					Size(objects[id].transform);
					break;
				case Type.alphaPlus:
					AlphaPlus(objects[id].GetComponent<Image>());
					break;
				case Type.alphaMinus:
					AlphaMinus(objects[id].GetComponent<Image>());
					break;
			}
			if(_time >= timeRepeat) {
				id++;
				if (id >= objects.Count) {
					start = false;
					CancelInvoke();
					@event.Invoke(new BaseEventData(EventSystem.current));
					yield break;
				}
				for (int i = 0; i < objects.Count; i++) {
					if (i == id) {
						objects[i].SetActive(true);
					} else {
						objects[i].SetActive(false);
					}
				}
				_time = 0;
			}
		}
		yield return new WaitForSecondsRealtime(deltaTime);
		StartCoroutine("UpdateAction"); 
	}

	void Size(Transform t) {
		t.localScale += Vector3.one * speed * deltaTime;
	}

	void AlphaPlus(Image i) {
		Color c = i.color;
		c.a += speed * deltaTime;
		i.color = c;
	}
	void AlphaMinus(Image i) {
		Color c = i.color;
		c.a -= speed * deltaTime;
		i.color = c;
	}
}
