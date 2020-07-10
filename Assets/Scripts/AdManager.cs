using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class AdManager : MonoBehaviour, IRewardedVideoAdListener
{
	public bool consentValue;
	public bool TestAppodeal;
	public bool isAd;
	public class RewardAd : UnityEvent<UnityEvent> { }
	public class Events {
		public RewardAd rewardAd = new RewardAd();
	}
	public static Events events = new Events();

	private UnityEvent callbackEvent;
	private bool viewReward;
	void Awake() {
		DontDestroyOnLoad(this);
	}
    void Start()
    {
		if (!isAd) return;
		Appodeal.initialize("5ec7062e1c74421f6f4a362d4fc9ab8b108e1f0027fd9599", Appodeal.REWARDED_VIDEO | Appodeal.INTERSTITIAL, consentValue);
		Appodeal.setRewardedVideoCallbacks(this);
		Appodeal.setTesting(TestAppodeal);
		InitializeEvent();
	}

	void InitializeEvent() {
		events.rewardAd.AddListener((UnityEvent ev) => {
			callbackEvent = ev;
			if (Appodeal.canShow(Appodeal.REWARDED_VIDEO)) {
				Appodeal.show(Appodeal.REWARDED_VIDEO);
				Debug.Log("Appodeal ShowReward");
			} else {
				Debug.Log("Appodeal NotShowReward");
			}
		});
	}


	#region RewardAd
	public void onRewardedVideoLoaded(bool precache) {
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoFailedToLoad() {
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoShowFailed() {
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoShown() {
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoFinished(double amount, string name) {
		callbackEvent.Invoke();
	}

	public void onRewardedVideoClosed(bool finished) {
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoExpired() {
		throw new System.NotImplementedException();
	}

	public void onRewardedVideoClicked() {
		throw new System.NotImplementedException();
	}
	#endregion
}
