using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.Events;

public class FacebookManager : MonoBehaviour {
	public class Events {
		public LevelCompleted levelCompleted = new LevelCompleted();
	}
	public static Events events = new Events();

	public class LevelCompleted : UnityEvent<int> { }
	void Awake() {
		UnityEventsActions();
		if (FB.IsInitialized) {
			FB.ActivateApp();
		} else {
			//Handle FB.Init
			FB.Init(() => {
				FB.ActivateApp();
			});
		}
		DontDestroyOnLoad(this);
	}

	void UnityEventsActions() {
		events.levelCompleted.AddListener((int number) => {
			LevelCompletedEvent(number);
		});
	}

	private void InitCallback() {
		if (FB.IsInitialized) {
			FB.ActivateApp();
		} else {
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}

	private void OnHideUnity(bool isGameShown) {
		if (!isGameShown) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	void LevelCompletedEvent(int number) {
		var parameters = new Dictionary<string, object>();
		parameters[AppEventParameterName.Level] = number;
		SendEvent(parameters, AppEventName.AchievedLevel);
	}

	void SendEvent(Dictionary<string, object> parameters, string name) {
		Debug.Log("Send Facebook Event: "+name);
		FB.LogAppEvent(name, parameters:parameters);
	}

}
