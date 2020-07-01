using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
	public static bool testPlay;
	public class StartGame : UnityEvent { }

	public class Events {
		public StartGame startGame = new StartGame();
	}
	public static Events events = new Events();

	public BonusIntDictionary Bonuses = BonusIntDictionary.New<BonusIntDictionary>();
	public static Dictionary<Bonus, int> bonuses;

	public static float speed;
	public static Dictionary<Bonus, int> countBonus = new Dictionary<Bonus, int>();
	public float _speed;
	public static Entity entityPlayer;

	[SerializeField]
	GameObject _coinPrefab;
	public static GameObject coinPrefab;

	[SerializeField]
	float _borderField;
	public static float borderField;

	public MainGeneration generate;
	public GameHUD hud;

	float _time;
	float sizeLevel;
	void InitializeEvent() {
		events.startGame.AddListener(() => {
			hud.StartBar(sizeLevel);
			generate.enabled = true;
			entityPlayer.GetComponent<PlayerController>().lockMove = false;
		});
	}

	void Awake() {
		InitializeEvent();
		if (!generate) {
			generate = GameObject.Find("GenerateManager").GetComponent<MainGeneration>();
		}
		generate.enabled = false;
		coinPrefab = _coinPrefab;
		speed = _speed;
		bonuses = Bonuses.dictionary;
		borderField = _borderField;
		StartLevel();
	}

	void StartLevel() {
		int l;
		if (testPlay) {
			l = LevelStateEditor.levelID;
		} else {
			l = GameManager.player.level;
		}
		LevelManager.Stage stage = LevelManager.stages.stages[l];
		for (int i = 0; i < stage.sheeps.Count; i++) {
			Entity entity = Enemies.enemies[stage.sheeps[i]];
			float precent = 0;
			float speed = 1;
			if(stage.precents.Count < i) {
				precent = stage.precents[i];
			}
			//if (stage.speedShips.Count < i) {
				speed = stage.speedShips[i];
			//}
			generate.enemies.Add(entity, precent);
			generate.speedEnemies.Add(entity, speed);
		}
		sizeLevel = stage.size;
		generate.k = stage.kTime;
		generate.kSpeed = stage.kSpeed;
		generate.avengerTime = stage.startTime;
		if (entityPlayer) {
			Destroy(entityPlayer.gameObject);
		}
		entityPlayer = Instantiate(GameManager.skins[GameManager.player.pack][GameManager.player.skins].prefab, Camera.main.transform).GetComponent<Entity>();
		entityPlayer.position = Vector2.zero;
		hud.StartLevel(stage.id);
	}

	private void Update() {
		Bonuses.dictionary = bonuses;
		_time += Time.deltaTime;
		if(_time >= sizeLevel) {
			GameManager.events.completeLEvel.Invoke(entityPlayer.coins);
		}
	}

	public void Start() {
		countBonus = new Dictionary<Bonus, int>();
		foreach(var i in bonuses) {
			countBonus.Add(i.Key, 0);
		}
		
	}
}
