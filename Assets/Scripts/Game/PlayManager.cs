using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayManager : MonoBehaviour
{
	public static bool testPlay;
	public class StartGame : UnityEvent { }
	public class Death : UnityEvent<int> { }
	public class Reborn : UnityEvent { }
	public class AdReborn : UnityEvent { }
	public class GameOver : UnityEvent { }
	public class NextLevel : UnityEvent { }
	public class DoubleCoins : UnityEvent { }
	public class AdDoubleCoins : UnityEvent { }
	public class Win : UnityEvent { }

	public class Events {
		public StartGame startGame = new StartGame();
		public Death death = new Death();
		public Reborn reborn = new Reborn();
		public GameOver gameOver = new GameOver();
		public NextLevel nextLevel = new NextLevel();
		public DoubleCoins doubleCoins = new DoubleCoins();
		public Win win = new Win();
		public AdReborn adReborn = new AdReborn();
		public AdDoubleCoins adDoubleCoins = new AdDoubleCoins();
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
	public static float globalBorderField;
	public static float globalVerticalField;

	public MainGeneration generate;
	public GameHUD hud;

	float _time;
	float sizeLevel;
	int coins;
	bool reborn = false;
	void InitializeEvent() {
		events.startGame.AddListener(() => {
			hud.StartBar(sizeLevel);
			generate.enabled = true;
			entityPlayer.GetComponent<PlayerController>().lockMove = false;
		});
		events.death.AddListener((int c) => {
			coins = c;
			if (!reborn) {
				hud.OnRebornMenu();
			} else {
				events.gameOver.Invoke();
			}
		});
		events.reborn.AddListener(() => {
			//Рекламма
			RebornPlayer();
			hud.OffRebornMenu();
			reborn = true;
		});
		events.gameOver.AddListener(() => {
			hud.OffRebornMenu();
			hud.DeathMenu();
			GameManager.events.gameOver.Invoke(coins);
		});
		events.doubleCoins.AddListener(()=> {
			hud.DoubleCoinsComplete();
			GameManager.events.addCoins.Invoke(coins);
			coins *=2;
			hud.winCoins.text = coins.ToString();
		});
		events.nextLevel.AddListener(() => {
			GameManager.events.nextLevel.Invoke();
		});
		events.win.AddListener(() => {
			GameManager.events.completeLEvel.Invoke(coins);
			hud.CompleteLevel();
		});
		events.adDoubleCoins.AddListener(() => {
			AdManager.events.rewardAd.Invoke(events.doubleCoins);
		});
		events.adReborn.AddListener(() => {
			AdManager.events.rewardAd.Invoke(events.reborn);
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
		globalBorderField = Camera.main.ViewportToWorldPoint(new Vector3(1, 0f)).x+_borderField;
		globalVerticalField = Camera.main.ViewportToWorldPoint(new Vector3(1, 0f)).y;
		StartLevel();
	}

	void StartLevel() {
		int l;
		l = GameManager.player.level;
		LevelManager.Stage stage = LevelManager.stages.stages[l];
		for (int i = 0; i < stage.sheeps.Count; i++) {
			MainGeneration.Ship ship = new MainGeneration.Ship(
				stage.sheeps[i],
				stage.precents[i],
				stage.speedShips[i],
				stage.lifes[i]);
			generate.enemies.Add(ship);
		}
		Dictionary<Bonus, int> d = new Dictionary<Bonus, int>();
		foreach(var i in bonuses.Keys) {
			d.Add(i, bonuses[i]);
		}
		foreach (var i in d) {
			if (i.Key.GetType() != typeof(BonusWeapon)) {
				Debug.Log(i.Key.name);
				bonuses[i.Key] = (int)Mathf.Round(i.Value * stage.kBonus);
			}
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

	void RebornPlayer() {
		entityPlayer.gameObject.SetActive(true);
		entityPlayer.Reborn();
		entityPlayer.position = Vector2.zero;
		BonusBuff b = null;
		foreach(var i in bonuses) {
			if (i.Key.TryGetComponent<BonusBuff>(out b)) {
				if (b.buff.nameBuff == "shield") {
					break;
				}
			}
			
		}
		entityPlayer.AddBuff(b.buff);
		entityPlayer.GetComponent<PlayerController>().lockMove = false;
	}

	private void Update() {
		Bonuses.dictionary = bonuses;
		_time += Time.deltaTime;
		if(_time >= sizeLevel) {
			generate.gameObject.SetActive(false);
			TotalKill._totalKill.GoTotalKill();
			coins = entityPlayer.coins;
			_time = int.MinValue;
		}
	}

	public void Start() {
		countBonus = new Dictionary<Bonus, int>();
		foreach(var i in bonuses) {
			countBonus.Add(i.Key, 0);
		}
		
	}
}


/*
 *	в худе событие игрока если жизней меньше 0 то отправка
 * события в плейменеджер, отпрпавлет деньги и 
 * сохраняет в переменную менеджера
 *	событие в плей менеджер открывает меню воскрешения через худ
 *	на кнопках функции из худа, либо реклама и воскрешение
 * либо полная смерть - это все события в менеджере
 *  
 * 
 */