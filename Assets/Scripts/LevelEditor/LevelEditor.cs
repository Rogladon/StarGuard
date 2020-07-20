using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class LevelEditor : MonoBehaviour
{
	[Header("Components")]
	public Transform levelPanel;
	public GameObject RemoveLevelPanel;

	[Header("Prefabs")]
	public GameObject prefabLevel;
	public GameObject prefabPlus;

	[Header("Other")]
	public float pointerDelay = 1f;

	private List<GameObject> objects = new List<GameObject>();
	private float _time;
	private int selectedLevel;

	private void Start() {
		PlayManager.testPlay = false;
		InitializeLeveles();
	}
	float _timeSave;
	private void Update() {
		_timeSave += Time.deltaTime;
		if (_timeSave > 1) {
			LevelManager.SaveLevelJson();
			_timeSave = 0;
		}
	}
	int id;
	private void InitializeLeveles() {
		foreach(var i in objects) {
			Destroy(i);
		}
		objects.Clear();
		id = 1;
		GameObject plus1 = Instantiate(prefabPlus, levelPanel);
		plus1.GetComponent<Button>().onClick.AddListener(() => { AddLevel(1); });
		objects.Add(plus1);
		foreach (var i in LevelManager.stages.stages) {
			i.id = id;
			
			GameObject l = Instantiate(prefabLevel, levelPanel);
			l.GetComponentInChildren<Text>().text = i.id.ToString();
			if (i.sheeps.Count == 0) {
				l.GetComponent<Image>().color = Color.red;
			}
			EventTrigger trigger = l.GetComponent<EventTrigger>();
			EventTrigger.Entry entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerDown;
			entry.callback.AddListener(data => { PointerDown((PointerEventData)data, i.id); });
			trigger.triggers.Add(entry);
			entry = new EventTrigger.Entry();
			entry.eventID = EventTriggerType.PointerUp;
			entry.callback.AddListener(data => { PointerUp((PointerEventData)data); });
			trigger.triggers.Add(entry);
			GameObject plus = Instantiate(prefabPlus, levelPanel);
			plus.GetComponent<Button>().onClick.AddListener(() => { AddLevel(i.id+1); });
			objects.Add(plus);
			objects.Add(l);
			id++;
		}
		
	}
	public void PointerUp(PointerEventData data) {
		CancelInvoke();
		if (_time < pointerDelay) {
			SelectLevel();
		}
	}
	public void PointerDown(PointerEventData data, int go) {
		selectedLevel = go;
		Debug.Log(go);
		_time = 0;
		InvokeRepeating("Timer", Time.fixedDeltaTime, Time.fixedDeltaTime);
	} 
	public void SelectLevel() {
		LevelStateEditor.levelID = selectedLevel-1;
		SceneManager.LoadScene(4);
	}

	public void AddLevel(int _id) {
		LevelManager.Stage stage = new LevelManager.Stage();
		stage.id = id;
		stage.sheeps = new List<int>();
		stage.precents = new List<int>();
		stage.speedShips = new List<float>();
		stage.size = 60;
		stage.kSpeed = 200;
		stage.kTime = 200;
		stage.kBonus = 1;
		stage.startTime = 2;
		LevelManager.stages.stages.Insert(_id - 1, stage);
		InitializeLeveles();
	}
	public void RemoveLevel() {
		RemoveLevelPanel.SetActive(false);
		int _id = selectedLevel;
		LevelManager.stages.stages.RemoveAt(_id - 1);
		InitializeLeveles();
	}
	public void CloseRemovePanel() {
		RemoveLevelPanel.SetActive(false);
	}
	void PrewRemoveLevel() {
		RemoveLevelPanel.SetActive(true);
	}
	public void ReIndex() {
		int _id = 1;
		foreach(var i in LevelManager.stages.stages) {
			i.id = _id;
			_id++;
		}
		InitializeLeveles();
	}
	public void Back() {
		GameManager.BackMenu();
	}
	public void Export() {

	}

	public void SaveJson() {
		LevelManager.SaveLevelJson();
	}

	void Timer() {
		_time += Time.fixedDeltaTime;
		if (_time >= pointerDelay) {
			PrewRemoveLevel();
		}
	}

	public void Send() {
		MailMessage message = new MailMessage();
		message.Subject = "Levels from "+SystemInfo.deviceName;
		message.Body = JsonUtility.ToJson(LevelManager.stages);
		message.From = new MailAddress("brozersofabsurd@gmail.com");
		message.To.Add("brozersofabsurd@gmail.com");
		message.BodyEncoding = System.Text.Encoding.Unicode;

		SmtpClient smtp = new SmtpClient();
		smtp.Host = "smtp.gmail.com";
		smtp.Port = 587;
		smtp.Credentials = new NetworkCredential(message.From.Address, "559448Regar");
		smtp.EnableSsl = true;

		ServicePointManager.ServerCertificateValidationCallback =
		 delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

		smtp.Send(message);
	}
}
