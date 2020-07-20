using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudConfig : MonoBehaviour
{
	public Transform checkAudio;
	public Transform checkVibro;

	bool muteAudio => AudioManager.muteAudio;
	bool muteVibro => AudioManager.muteVibro;

	private void Update() {
		if (muteAudio) {
			checkAudio.localScale = new Vector3(-1,1,1);
		} else {
			checkAudio.localScale = new Vector3(1, 1, 1);
		}
		if (muteVibro) {
			checkVibro.localScale = new Vector3(-1, 1, 1);
		} else {
			checkVibro.localScale = new Vector3(1, 1, 1);
		}
	}

	public void BtnAudio() {
		AudioManager.events.muteAudio.Invoke(!muteAudio);
	}
	public void BtnVibro() {
		AudioManager.events.muteVibro.Invoke(!muteVibro);
	}
}
