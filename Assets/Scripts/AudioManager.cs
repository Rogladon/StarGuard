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
	public class Vibro : UnityEvent { }
	public class MuteAudio : UnityEvent<bool> { }
	public class MuteVibro : UnityEvent<bool> { }

	public class Events {
		public MainMenuTrack mainMenuTrack = new MainMenuTrack();
		public GameTrack gameTrack = new GameTrack();
		public PlayerShoot playerShoot = new PlayerShoot();
		public Hit hit = new Hit();
		public Vibro vibro = new Vibro();
		public MuteAudio muteAudio = new MuteAudio();
		public MuteVibro muteVibro = new MuteVibro();
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
	public static bool muteAudio;
	public static bool muteVibro;

	public void Awake() {
		DontDestroyOnLoad(this);
		audioSource = GetComponent<AudioSource>();
		InitilizeEvents();
		if (PlayerPrefs.HasKey("muteAudio")) {
			if(!bool.TryParse(PlayerPrefs.GetString("muteAudio"), out muteAudio)) {
				muteAudio = false;
			}
		} else {
			muteAudio = false;
		}

		if (PlayerPrefs.HasKey("muteVibro")) {
			if (!bool.TryParse(PlayerPrefs.GetString("muteVibro"), out muteVibro)) {
				muteVibro = false;
			}
		} else {
			muteVibro = false;
		}
	}
	float _time;
	float maxTimeShoot = 0.1f;
	void InitilizeEvents() {
		events.gameTrack.AddListener(() => {
			if (audioSource.clip != typeAudioTrack[TypeTrack.gameTrack]) {
				audioSource.clip = typeAudioTrack[TypeTrack.gameTrack];
				
			}
			if (!audioSource.isPlaying) {
				audioSource.loop = true;
				audioSource.Play();
			}
		});
		events.mainMenuTrack.AddListener(() => {
			if (audioSource.clip != typeAudioTrack[TypeTrack.mainMenuTrack]) {
				audioSource.clip = typeAudioTrack[TypeTrack.mainMenuTrack];
			}
			if (!audioSource.isPlaying) {
				audioSource.loop = true;
				audioSource.Play();
			}
		});
		events.playerShoot.AddListener((int type) => {
			if (_time < maxTimeShoot) return;
			_time = 0;
			switch (type) {
				case 0:
					audioSource.PlayOneShot(typeAudioTrack[TypeTrack.playerShoot], 1f);
					break;
				default:
					audioSource.PlayOneShot(typeAudioTrack[TypeTrack.playerShoot], 1f);
					break;
			}

		});
		events.hit.AddListener(() => {
			audioSource.PlayOneShot(typeAudioTrack[TypeTrack.hit],2f);
		});
		events.vibro.AddListener(() => {
			if(!muteVibro)
				Handheld.Vibrate();
		});
		events.muteAudio.AddListener((bool b) => {
			muteAudio = b;
			PlayerPrefs.SetString("muteAudio", b.ToString());
		});
		events.muteVibro.AddListener((bool b) => {
			muteVibro = b;
			PlayerPrefs.SetString("muteVibro", b.ToString());
		});
	}

	private void Update() {
		_time += Time.deltaTime;
		audioSource.mute = muteAudio;
	}
}
