using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
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

	void Awake() {
		entityPlayer = Instantiate(GameManager.skins[GameManager.player.pack][GameManager.player.skins].prefab).GetComponent<Entity>();
		entityPlayer.position = Vector2.zero;
		coinPrefab = _coinPrefab;
		speed = _speed;
		bonuses = Bonuses.dictionary;
		borderField = _borderField;
	}

	private void Update() {
		Bonuses.dictionary = bonuses;
	}

	public void Start() {
		countBonus = new Dictionary<Bonus, int>();
		foreach(var i in bonuses) {
			countBonus.Add(i.Key, 0);
		}
		
	}
}
