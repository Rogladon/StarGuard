using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	public class ChoiceSkin : UnityEvent<int, int> { }
	public class BySkin : UnityEvent<int, int> { }
	public class EndGame : UnityEvent<int> { }
	public class Events {
		public ChoiceSkin choiceSkin = new ChoiceSkin();
		public BySkin bySkin = new BySkin();
		public EndGame endGame = new EndGame();
	}
	public static Events events = new Events();
	public class Player {
		public int pack;
		public int skins;
		public int coins;
		public int level;
	}
	public static Player player = new Player();
	public static Skin currentSkin { get {
			return skins[player.pack][player.skins];
		} }
	public static List<List<Skin>> skins;
	public static List<int> openSkins = new List<int>();

	[SerializeField]
	private List<Skin> _skins;



	private void Awake() {
		Upload();
		DontDestroyOnLoad(this);
		skins = new List<List<Skin>>();
		skins.Add(new List<Skin>());
		skins.Add(new List<Skin>());
		skins.Add(new List<Skin>());
		for (int i = 0; i < _skins.Count; i++) {
			_skins[i].globalID = i;
			if (openSkins.Contains(i)) {
				_skins[i].has = 1;
			}
			skins[_skins[i].pack].Add(_skins[i]);
		}
		for (int i = 0; i < skins.Count; i++) {
			for (int j = 0; j < skins[i].Count; j++) {
				skins[i][j].index = j;
			}
		}
		events.choiceSkin.AddListener((int pack, int id) => {
			player.skins = id;
			player.pack = pack;
			Save();
		});
		events.bySkin.AddListener((int pack, int id) => {
			skins[pack][id].has = 1;
			openSkins.Add(skins[pack][id].globalID);
			Save();
		});
		events.endGame.AddListener((int coins) => {
			player.coins += coins;
			Save();
		});
		//TOODO
		events.choiceSkin.Invoke(0, 0);

		//TODOO
	}

	class OpenSkills {
		public List<int> openSkins = new List<int>();
	}
	public void Save() {
		string json = JsonUtility.ToJson(player);
		PlayerPrefs.SetString("player", json);
		OpenSkills os = new OpenSkills();
		os.openSkins = openSkins;
		string osj = JsonUtility.ToJson(os);
		PlayerPrefs.SetString("openSkins", osj);
	}

	public void Upload() {
		if (PlayerPrefs.HasKey("player")) {
			player = JsonUtility.FromJson<Player>(PlayerPrefs.GetString("player"));
		}
		if (PlayerPrefs.HasKey("openSkins")) {
			OpenSkills os = JsonUtility.FromJson<OpenSkills>(PlayerPrefs.GetString("openSkins"));
			openSkins = os.openSkins;
		} else {
			openSkins.Add(0);
		}
	}

	public void Start() {
		SceneManager.LoadScene(1);
	}


	public static void LoadLevel() {
		//TOODO
		SceneManager.LoadScene(2);
	}
	public static void BackMenu() {
		SceneManager.LoadScene(1);
	}
}
