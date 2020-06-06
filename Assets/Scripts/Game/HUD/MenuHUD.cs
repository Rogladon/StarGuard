using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class MenuHUD : MonoBehaviour
{
	public GameObject mainMenu;
	public GameObject mall;
	public Animator animatorDoor;
	public class OpenMall : UnityEvent { }
	public class CloseMall : UnityEvent { }

	public class Events {
		public OpenMall openMall = new OpenMall();
		public CloseMall closeMall = new CloseMall();
	}
	public static Events events = new Events();
	private void Start() {
		events.openMall.AddListener(() => {
			mall.SetActive(true);
			mainMenu.SetActive(false);
		});

		events.closeMall.AddListener(() => {
			mall.SetActive(false);
			mainMenu.SetActive(true);
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
	public void Menu() {
		animatorDoor.SetTrigger("Close");
	}
}
