using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioManager : MonoBehaviour
{
    public class MainMenuTrack : UnityEvent { }
	public class GameTrack : UnityEvent { }
	public class PlayerShoot : UnityEvent<int> { }
	public class Hit : UnityEvent { }

	public class Events {
		public MainMenuTrack mainMenuTrack = new MainMenuTrack();
		public GameTrack gameTrack = new GameTrack();
		public PlayerShoot playerShoot = new PlayerShoot();
		public Hit hit = new Hit();
	}

	public static Events events = new Events();

	public enum TypeTrack {
		mainMenuTrack,
		gameTrack,
		playerShoot,
		hit
	}

	public TypeAudioTrackDictionary TypeAudioTrack = TypeAudioTrackDictionary.New<TypeAudioTrackDictionary>();
	public  Dictionary<TypeTrack, AudioClip> typeAudioTrack { get { return TypeAudioTrack.dictionary; } }

	public AudioSource audioSource;

	public void Awake() {
		DontDestroyOnLoad(this);
		audioSource = GetComponent<AudioSource>();
		InitilizeEvents();

	}
	float _time;
	float maxTimeShoot = 0.1f;
	void InitilizeEvents() {
		events.gameTrack.AddListener(() => {
			audioSource.clip = typeAudioTrack[TypeTrack.gameTrack];
			audioSource.loop = true;
			audioSource.Play();
		});
		events.mainMenuTrack.AddListener(() => {
			audioSource.clip = typeAudioTrack[TypeTrack.mainMenuTrack];
			audioSource.Play();
		});
		events.playerShoot.AddListener((int type) => {
			if (_time < maxTimeShoot) return;
			_time = 0;
			switch (type) {
				case 0:
					audioSource.PlayOneShot(typeAudioTrack[TypeTrack.playerShoot], 0.5f);
					break;
				default:
					audioSource.PlayOneShot(typeAudioTrack[TypeTrack.playerShoot], 0.5f);
					break;
			}

		});
		events.hit.AddListener(() => {
			audioSource.PlayOneShot(typeAudioTrack[TypeTrack.hit],0.5f);
		});
	}

	private void Update() {
		_time += Time.deltaTime;
	}
}
