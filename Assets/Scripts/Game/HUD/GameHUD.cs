using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour {
	[Header("Components")]
	public Transform lifePanel;
	public Text textCoin;
	public GameObject pauseMenu;
	public GameObject deathMenu;
	private Entity entity;
	Transform cam;


	[Header("Prefabs")]
	public GameObject lifeIcon;

	public float ShakeFactor = 2f;

	float shakeTimer = 0;
	float shakeAmplitude = 0.25f;
	float maxShakeAmplitude { get { return shakeAmplitude * 4; } }
	float shakeTimerDecrisePercent = 0.5f;


	private List<GameObject> lifesGO = new List<GameObject>();
	public void Start() {
		cam = Camera.main.transform;
		entity = PlayManager.entityPlayer;
		for (int i = 0; i < entity.maxLifes; i++) {
			GameObject go = Instantiate(lifeIcon, lifePanel);
			lifesGO.Add(go);
		}
		entity.events.lifeEvent.AddListener((int life) => {
			foreach(var  i in lifesGO) {
				Destroy(i);
			}
			if(life <= 0) {
				DeathMenu();
				return;
			}
			for (int i = 0; i < life; i++) {
				GameObject go = Instantiate(lifeIcon, lifePanel);
				lifesGO.Add(go);
			}
			StartShakeCamera(ShakeFactor);
			Handheld.Vibrate();
		});
	}

	void ShakeCamera() {
		if (shakeTimer < 0.0001f) shakeTimer = 0;
		shakeTimer = System.Math.Min(maxShakeAmplitude, shakeTimer);
		cam.transform.position = new Vector3(Random.Range(-shakeTimer, shakeTimer), Random.Range(-shakeTimer, shakeTimer), cam.transform.position.z);
		shakeTimer *= (1 - shakeTimerDecrisePercent);
	}

	public void StartShakeCamera(float factor = 1f) {
		shakeTimer = shakeAmplitude * factor;
	}

	private void Update() {
		ShakeCamera();
		textCoin.text = entity.coins.ToString();
	}

	public void Pause() {
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
	}

	public void DeathMenu() {
		Time.timeScale = 0;
		deathMenu.SetActive(true);
	}

	public void Continue() {
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
	}
	public void Restart() {
		Continue();
		GameManager.LoadLevel();
	}
	public void Back() {
		if (entity) {
			GameManager.events.endGame.Invoke(entity.coins);
		}
		Continue();
		GameManager.BackMenu();
	}
}
