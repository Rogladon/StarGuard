﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MenuHUD : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject mall;
	public Animator animatorDoor;
	public GameObject config;
	public class OpenDoor : UnityEvent { }

	public class Events {
		public OpenDoor openDoor = new OpenDoor();
	}
	public static Events events = new Events();
	public bool openMall = false;
	private void Start() {
		AudioManager.events.mainMenuTrack.Invoke();
		events.openDoor.AddListener(() => {
			if (!openMall) {
				Debug.Log("OpenMall");
				mall.SetActive(true);
				mainMenu.SetActive(false);
				openMall = true;
			} else {
				Debug.Log("CloseMall");
				mall.SetActive(false);
				mainMenu.SetActive(true);
				openMall = false;
			}
			animatorDoor.SetTrigger("Close");
		});
	}
	public float speedScrool;
	public void Update() {
		//if (InputHelper.isMoved) {
		//	if(InputHelper.deltaPos.x > speedScrool) {
		//		Mall();
		//	}
		//	if(InputHelper.deltaPos.x < -speedScrool) {
		//		//Страничка разработчика
		//	}
		//}
	}

	public void Play() {
		GameManager.LoadLevel();
	}

	public void Mall() {
		animatorDoor.SetTrigger("Open");
	}
	public void LevelEditor() {
		SceneManager.LoadScene(3);
	}
	public void OpenConfig() {
		config.SetActive(true);
	}
	public void CancelConfig() {
		config.SetActive(false);
	}
	public void OpenURL(string url) {
		Application.OpenURL(url);
	}
}
