using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mall : MonoBehaviour
{
	[Header("Const")]
	public int maxCountChoice;
	public float startTime;
	public float deltaTime;
	[Header("Components")]
	public GameObject panel;
	public Transform domain;
	public Button btnBy;
	public GameObject choiceWindow;
	private int curPack = 0;
	private List<Item> items = new List<Item>();
	public GameObject item;
	public Text textMoney;
	public int price;

	private GameObject emptyItem;

	private void OnEnable() {
		textMoney.text = GameManager.player.coins.ToString();
		InstItem(curPack);
		InstSheep(GameManager.currentSkin);
	}
	private void OnDisable() {
		DestroyDomain();
	}


	void InstItem(int pack) {
		choiceWindow.SetActive(false);
		if (GameManager.player.coins >= price) {
			btnBy.interactable = true;
		} else {
			btnBy.interactable = false;
		}
		foreach (var i in items) {
			Destroy(i.gameObject);
		}
		Destroy(emptyItem);
		items.Clear();
		var skins = GameManager.skins;
		for (int j = 0; j < skins[pack].Count; j++) {
			Item go = Instantiate(item, panel.transform).GetComponent<Item>();
			go.GetComponent<Item>().Init(skins[pack][j], this);
			items.Add(go);
			if(j == 0) {
				go = Instantiate(item, panel.transform).GetComponent<Item>();
				go.icon.enabled = false;
				go.btn.enabled = false;
				emptyItem = go.gameObject;
			}
		}
	}

	public void InstSheep(Skin s) {

		DestroyDomain();
		GameObject go = Instantiate(s.prefab, domain);
		go.GetComponent<PlayerController>().enabled = false;
	}

	void DestroyDomain() {
		foreach (Transform i in domain) {
			Destroy(i.gameObject);
		}
	}

	public void BySheep() {
		GameManager.player.coins -= price;
		textMoney.text = GameManager.player.coins.ToString();
		choiceWindow.SetActive(true);
		_time = startTime;
		closeItems = new List<Item>();
		foreach(var i in items) {
			if (!i.has) {
				closeItems.Add(i);
			}
		}
		if(closeItems.Count > 0) btnBy.interactable = false;
		ChoiceBy();
	}
	List<Item> closeItems;
	int lastRand = -1;
	int countChoice = 0;
	float _time;
	void ChoiceBy() {
		if(closeItems.Count <= 1) {
			By(closeItems[0]);
			return;
		}
		int rand;
		do {
			rand = Random.Range(0, closeItems.Count);
		} while (rand == lastRand);
		lastRand = rand;
		choiceWindow.transform.position = closeItems[rand].transform.position;
		if (countChoice >= maxCountChoice || closeItems.Count<=1) {
			By(closeItems[rand]);
		} else {
			Invoke("ChoiceBy", _time);
			_time += deltaTime;
		}
		countChoice++;
	}

	void By(Item item) {
		item.BySelf();
		countChoice = 0;
		InstItem(curPack);
	}
}
