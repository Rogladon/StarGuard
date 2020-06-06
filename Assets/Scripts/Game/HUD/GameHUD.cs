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


	[Header("Prefabs")]
	public GameObject lifeIcon;


	private List<GameObject> lifesGO = new List<GameObject>();
	public void Start() {
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
		});
	}

	private void Update() {
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
