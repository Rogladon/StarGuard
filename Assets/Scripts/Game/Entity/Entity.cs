using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Entity : MonoBehaviour {
	public bool player = false;
	[Header("Stats")]
	public int maxLifes;
	public float speed;
	public float kReload = 1;
	public Weapon.TypeMissle defaulTypeMIssle;

	[Header("Components")]
	public List<Weapon> staticWeapons;
	public Transform buffsDomain;
	public Transform weaponsDomain;
	public MissleTypeGameObjectDictionary TypeMissles = MissleTypeGameObjectDictionary.New<MissleTypeGameObjectDictionary>();
	public Dictionary<Weapon.TypeMissle, Missle> missles { get { return TypeMissles.dictionary; } }
	public Transform left;
	public Transform right;
	private PlayerController pc;

	[Header("Hidden")]
	[SerializeField]
	public List<Weapon> weapons;
	[SerializeField]
	public List<Buff> buffs;

	[Header("Other")]
	[SerializeField]
	public int coins;
	[HideInInspector]
	public int shield;
	private int _lifes;
	private int lifes {
		get {
			return _lifes;
		}
		set {
			if (shield > 0) {
				return;
			}
			_lifes = value;
			if (player) {
				events.lifeEvent.Invoke(_lifes);
			}
		}
	}
	public Vector2 position {
		get {
			return (Vector2)transform.position;
		}
		set {
			transform.position = (Vector3)value;
		}
	}

	public Vector2 directon {
		get {
			return transform.up;
		}
		set {
			transform.up = value;
		}
	}
	public bool death {
		get {
			if (lifes <= 0) {
				return true;
			} else {
				return false;
			}
		}
	}
	public class HitEvent : UnityEvent<int> { }
	public class Events {
		public HitEvent lifeEvent = new HitEvent();
	}
	public Events events = new Events();


	private void Start() {
		if (TryGetComponent(out pc)) {
			player = true;
		}
		lifes = maxLifes;
	}

	public void Reborn() {
		lifes = maxLifes;
	}

	public void DoHit(int damage) {
		if(shield > 0) {
			shield--;
			return;
		}
		lifes -= damage;
		AudioManager.events.hit.Invoke();
	}

	public void AddBonus(Bonus bonus) {
		if (bonus.GetType() == typeof(BonusWeapon)) {
			AddWeapon(((BonusWeapon)bonus).weapon);
		}
		if (bonus.GetType() == typeof(BonusBuff)) {
			AddBuff(((BonusBuff)bonus).buff);
		}
	}

	private void Update() {
		if (death) {
			OnDestroyThis();
		}
	}
	Vector2 dl;
	Vector2 dr;
	public void AddWeapon(Weapon _weapon) {
		Weapon weaponLeft = Instantiate(_weapon.gameObject, weaponsDomain).GetComponent<Weapon>();
		Weapon weaponRight = Instantiate(_weapon.gameObject, weaponsDomain).GetComponent<Weapon>();
		weapons.Insert(0, weaponLeft);
		weapons.Add(weaponRight);
		Vector2 dirleft = ((Vector2)left.position - staticWeapons[0].position) / (weapons.Count / 2 + 1);
		Vector2 dirright = ((Vector2)right.position - staticWeapons[0].position) / (weapons.Count / 2 + 1);
		dl = dirleft;
		dr = dirright;
		int i = 0;
		for (i = 0; i < weapons.Count / 2; i++) {
			weapons[i].position = staticWeapons[0].position + dirleft * (weapons.Count / 2 - i);
		}
		for (int j = i; j < weapons.Count; j++) {
			weapons[j].position = staticWeapons[staticWeapons.Count - 1].position + (dirright * (j - i + 1));
		}
		foreach (var w in weapons) {
			w.ResetTimer();
		}
		foreach (var w in staticWeapons) {
			w.ResetTimer();
		}
	}

	public void AddBuff(Buff buff) {
		GameObject go = Instantiate(buff.gameObject, buffsDomain);
		buffs.Add(go.GetComponent<Buff>());

	}


	void Death() {
		OnDestroyThis();
	}

	private void OnDestroyThis() {

		if (player) {
			gameObject.SetActive(false);
			return;
		}

		Destroy(gameObject);
		for (int i = 0; i < coins; i++) {
			GameObject go = Instantiate(PlayManager.coinPrefab);
			go.transform.position = transform.position + new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f), 0);
		}


		Bonus currentBonus = PlayManager.bonuses.Keys.GetEnumerator().Current;
		int minPrecent = int.MaxValue;
		Dictionary<Bonus, int> keyValues = new Dictionary<Bonus, int>();
		foreach (var i in PlayManager.bonuses) {
			int p = Random.Range(0, Mathf.RoundToInt(100 / i.Value));
			keyValues.Add(i.Key, p);
			if (p < minPrecent) {
				currentBonus = i.Key;
				minPrecent = p;
			}
		}
		if (minPrecent == 1) {
			if (PlayManager.countBonus[currentBonus] > 3) return;
			GameObject go = Instantiate(currentBonus.gameObject);
			go.transform.position = transform.position;
			if (currentBonus.GetType() == typeof(BonusWeapon)) {
				PlayManager.bonuses[currentBonus] -= PlayManager.bonuses[currentBonus] / 2;
				PlayManager.countBonus[currentBonus]++;
			}
		}
		
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if (player) return;
		if (other.CompareTag("Player")) {
			Entity entity;
			if (other.TryGetComponent(out entity)) {
				entity.DoHit(1);
				OnDestroyThis();
			}
		}
		if (other.CompareTag("KillObject")) {
			OnDestroyThis();
		}
	}

	#region BUFFS
	public void RemoveBuff(Buff buff) {
		if (buffs.Contains(buff)) {
			buffs.Remove(buff);
			Destroy(buff.gameObject);
		}
	}
	#endregion

}
