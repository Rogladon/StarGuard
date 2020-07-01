using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelStateEditor : MonoBehaviour
{
	public static int levelID;
	[Header("Components")]
	public GameObject sheepEditor;
	public GameObject mainStateEditor;
	public Text NameLevel;
	public Transform selectedShipPanel;
	public Transform shipPanel;
	public GameObject editPanel;
	public Slider sliderPrecent;
	public Slider sliderSpeed;
	public GameObject removeShipPanel;
	public Text nameEditPanel;
	[Header("Prefabs")]
	public GameObject item;

	private LevelManager.Stage stage;
	float _time;
	public float pointerDelay = 1f;
	int removedShip;
	List<GameObject> objects = new List<GameObject>();
	int idStage;

	private void Start() {
		NameLevel.text = "Level " + levelID;
		stage = LevelManager.stages.stages[levelID];
		InitializeShip();
		InitilizeSelectedShip();
	}
	float _timeSave;
	private void Update() {
		_timeSave += Time.deltaTime;
		if (_timeSave > 3) {
			LevelManager.SaveLevelJson();
			_timeSave = 0;
		}
		sliderPrecent.GetComponentInChildren<Text>().text = ((int)sliderPrecent.value).ToString();
		sliderSpeed.GetComponentInChildren<Text>().text = (sliderSpeed.value).ToString("N2");
	}
	List<Entity> enemies = Enemies.enemies;
	private void InitializeShip() {
		
		for(int i =0; i < enemies.Count; i++) {
			GameObject go = Instantiate(item, shipPanel);
			go.name = i.ToString();
			go.GetComponent<Image>().sprite = enemies[i].GetComponentInChildren<SpriteRenderer>().sprite;
			go.GetComponentInChildren<Text>().text = enemies[i].name;
			EventTrigger trigger = go.GetComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerClick;
			entry.callback.AddListener(data => { SelectShip((PointerEventData)data, go.name); });
			trigger.triggers.Add(entry);
		}
	}
	void InitilizeSelectedShip() {
		foreach(var i in objects) {
			Destroy(i);
		}
		objects.Clear();
		for (int i = 0; i < stage.sheeps.Count; i++) {
			GameObject go = Instantiate(item, selectedShipPanel);
			go.name = i.ToString();
			go.GetComponent<Image>().sprite = enemies[stage.sheeps[i]].GetComponentInChildren<SpriteRenderer>().sprite;
			go.GetComponentInChildren<Text>().text = enemies[stage.sheeps[i]].name;
			EventTrigger trigger = go.GetComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback.AddListener(data => { PointerDown((PointerEventData)data, go.name); });
			trigger.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerUp;
			entry.callback.AddListener(data => { PointerUp((PointerEventData)data); });
			trigger.triggers.Add(entry);
			objects.Add(go);
		}
	}
	public void Back() {
		SceneManager.LoadScene(3);
	}
	public void SelectShip(PointerEventData data, string _id) {
		int id = int.Parse(_id);
		stage.sheeps.Add(id);
		stage.precents.Add(50);
		stage.speedShips.Add(2);
		InitilizeSelectedShip();
		idStage = stage.sheeps.Count - 1;
		EditShip();
	}
	public void EditShip() {
		editPanel.SetActive(true);
		nameEditPanel.text = enemies[stage.sheeps[idStage]].name;
		sliderPrecent.value = stage.precents[idStage];
		sliderSpeed.value = stage.speedShips[idStage];
	}
	public void CancelEditPanel() {
		editPanel.SetActive(false);
	}
	public void SaveEditPanel() {
		editPanel.SetActive(false);
		stage.precents[idStage] = (int)sliderPrecent.value;
		stage.speedShips[idStage] = sliderSpeed.value;
	}
	public void PrewRemoveShip() {
		removeShipPanel.SetActive(true);
	}
	public void CancelRemovePanel() {
		removeShipPanel.SetActive(false);
	}
	public void RemoveShip() {
		CancelRemovePanel();
		stage.sheeps.RemoveAt(idStage);
		stage.precents.RemoveAt(idStage);
		InitilizeSelectedShip();
	}

	public void PointerUp(PointerEventData data) {
		CancelInvoke();
		if (_time < pointerDelay) {
			EditShip();
		}
	}
	public void PointerDown(PointerEventData data, string go) {
		idStage = int.Parse(go);
		Debug.Log(go);
		_time = 0;
		InvokeRepeating("Timer", Time.fixedDeltaTime, Time.fixedDeltaTime);
	}

	void Timer() {
		_time += Time.fixedDeltaTime;
		if (_time >= pointerDelay) {
			PrewRemoveShip();
		}
	}
	public void Play() {
		PlayManager.testPlay = true;
		GameManager.LoadLevel();
		Debug.Log(LevelManager.stages.stages[idStage].speedShips[0]);
	}
	public void MainState() {
		sheepEditor.SetActive(false);
		mainStateEditor.SetActive(true);
	}
	public void Sheep() {
		sheepEditor.SetActive(true);
		mainStateEditor.SetActive(false);
	}

}
