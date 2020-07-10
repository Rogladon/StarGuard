using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHUD : MonoBehaviour {
	[Header("Components")]
	public Transform lifePanel;
	public Text textCoins;
	public Text diedCoins;
	public Text winCoins;
	public GameObject pauseMenu;
	public GameObject deathMenu;
	public GameObject rebornMenu;
	public GameObject winMenu;
	public Text stage;
	public Text num;
	public Transform barSheep;
	public GameObject bar;
	private Entity entity { get { return PlayManager.entityPlayer; } }
	Transform cam;


	[Header("Prefabs")]
	public GameObject lifeIcon;
	

	public float ShakeFactor = 2f;
	public float timeStartNums = 1f;

	float shakeTimer = 0;
	float shakeAmplitude = 0.25f;
	float maxShakeAmplitude { get { return shakeAmplitude * 4; } }
	float shakeTimerDecrisePercent = 0.5f;
	bool startLevel = false;
	bool startBar = false;


	private List<GameObject> lifesGO = new List<GameObject>();
	public void Start() {
		cam = Camera.main.transform;
		for (int i = 0; i < entity.maxLifes; i++) {
			GameObject go = Instantiate(lifeIcon, lifePanel);
			lifesGO.Add(go);
		}
		entity.events.lifeEvent.AddListener((int life) => {
			foreach(var  i in lifesGO) {
				Destroy(i);
			}
			if(life <= 0) {
				PlayManager.events.death.Invoke(entity.coins);
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
	private int nums = 3;
	private float _time;
	private float speedSizes;
	private float speedBar;
	private void Update() {
		ShakeCamera();
		textCoins.text = entity.coins.ToString();
		if (startLevel) {
			_time += Time.deltaTime;
			num.transform.localScale += Vector3.one * speedSizes*Time.deltaTime;
			if(_time >= timeStartNums) {
				if(nums == 1) {
					PlayManager.events.startGame.Invoke();
					startLevel = false;
					stage.gameObject.SetActive(false);
					num.gameObject.SetActive(false);
					return;
				}
				_time = 0;
				nums--;
				num.text = nums.ToString();
				num.transform.localScale = Vector3.one;
			}
		}
		if (startBar) {
			barSheep.position += Vector3.up * speedBar*Time.deltaTime;
		}
	}

	public void Pause() {
		entity.GetComponent<PlayerController>().lockMove = true;
		Time.timeScale = 0;
		pauseMenu.SetActive(true);
	}

	public void DeathMenu() {
		diedCoins.text = entity.coins.ToString();
		Time.timeScale = 0;
		deathMenu.SetActive(true);
	}

	public void Continue() {
		entity.GetComponent<PlayerController>().lockMove = false;
		Time.timeScale = 1;
		pauseMenu.SetActive(false);
	}
	public void OnRebornMenu() {
		Time.timeScale = 0;
		rebornMenu.SetActive(true);
	}
	public void OffRebornMenu() {
		Time.timeScale = 1;
		rebornMenu.SetActive(false);
	}
	public void Reborn() {
		PlayManager.events.adReborn.Invoke();
	}
	public void NoReborn() {
		PlayManager.events.gameOver.Invoke();
	}

	public void CompleteLevel() {
		entity.GetComponent<PlayerController>().lockMove = true;
		Time.timeScale = 0;
		winCoins.text = entity.coins.ToString();
		winMenu.SetActive(true);
	}
	public void DoubleCoins(GameObject g) {
		g.SetActive(false);
		PlayManager.events.adDoubleCoins.Invoke();
	}
	public void NextLevel() {
		Time.timeScale = 1;
		PlayManager.events.nextLevel.Invoke();
	}
	public void Restart() {
		Continue();
		GameManager.LoadLevel();
	}
	public void Back() {
		//if (entity) {
		//	GameManager.events.endGame.Invoke(entity.coins);
		//}
		Continue();
		GameManager.BackMenu();
	}
	public void StartLevel(int level) {
		stage.gameObject.SetActive(true);
		stage.text = "STAGE " + level.ToString();
		num.gameObject.SetActive(true);
		_time = 0;
		startLevel = true;
		nums = 3;
		num.transform.localScale = Vector3.one;
		speedSizes = (1f) / timeStartNums;
	}
	public void StartBar(float sizeLevel) {
		float size = bar.GetComponent<BoxCollider2D>().size.y*bar.transform.localScale.y;
		speedBar = size / sizeLevel;
		startBar = true;
	}
}
