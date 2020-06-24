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
		public int kTime;
		public int kSpeed;
		public int size;
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
		stages = JsonUtility.FromJson<Stages>(json.text);
		Debug.Log(stages.stages[0].id);
	}
}
