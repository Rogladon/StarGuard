using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalKill : MonoBehaviour
{
	public static TotalKill _totalKill;
	public Explose explose;
	public float timeRepaet;
	public int repeat;
	public bool totalKill;
	private float _time;
	private int i = 0;
	public void Start() {
		_totalKill = this;
	}
	public void Update() {
		if (!totalKill || i >= repeat) return;
		_time += Time.deltaTime;
		if(_time >= timeRepaet) {
			_time = 0;
			i++;
			GameObject go = Instantiate(explose.gameObject);
			Vector2 pos = new Vector2();
			pos.x = Random.Range(-PlayManager.globalBorderField, PlayManager.globalBorderField);
			pos.y = Random.Range(-PlayManager.globalVerticalField/2, PlayManager.globalVerticalField/2);
			go.transform.position = pos;
		}
		if(i >= repeat) {
			PlayManager.events.win.Invoke();
		}
	}
	public void GoTotalKill() {
		totalKill = true;
	}
}
