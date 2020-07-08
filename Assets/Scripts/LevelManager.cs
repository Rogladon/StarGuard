using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.MiniJSON;

public class LevelManager : MonoBehaviour
{
	[SerializeField]
	[System.Serializable]
    public class Stage {
		public int id;
		public List<int> sheeps = new List<int>();
		public List<int> precents = new List<int>();
		public List<int> lifes = new List<int>();
		public List<float> speedShips = new List<float>();
		public int kTime;
		public int kSpeed;
		public int size;
		public float startTime;
	}
	[SerializeField]
	[System.Serializable]
	public class Stages {
		public List<Stage> stages = new List<Stage>();
	}
	[SerializeField]
	public static Stages stages;

	public TextAsset json;
	public void Awake() {
		if (PlayerPrefs.HasKey("LevelJson")) {
			stages = JsonUtility.FromJson<Stages>(PlayerPrefs.GetString("LevelJson"));
		} else {
			stages = JsonUtility.FromJson<Stages>(json.text);

		}
	}

	public static void SaveLevelJson() {
		string str = JsonUtility.ToJson(stages);
		PlayerPrefs.SetString("LevelJson", str);
	}
	[ContextMenu("DeleteLevel")]
	void DeleteLevels() {
		PlayerPrefs.DeleteKey("LevelJson");
	}
}
