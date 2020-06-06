using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public void OpenDoor() {
		MenuHUD.events.openMall.Invoke();
	}
	public void CloseDoor() {
		MenuHUD.events.closeMall.Invoke();
	}
}
