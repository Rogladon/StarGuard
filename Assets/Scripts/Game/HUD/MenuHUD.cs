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
		public OpenMall openDoor = new OpenMall();
	}
	public static Events events = new Events();
	public bool openMall = true;
	private void Start() {
		events.openDoor.AddListener(() => {
			if (openMall) {
				mall.SetActive(true);
				mainMenu.SetActive(false);
				openMall = false;
			} else {
				mall.SetActive(false);
				mainMenu.SetActive(true);
				openMall = true;
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
}
