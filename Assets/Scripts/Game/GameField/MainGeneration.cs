using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGeneration : MonoBehaviour {
	public EntityTimeDictionary Enemies = EntityTimeDictionary.New<EntityTimeDictionary>();
	public Dictionary<Entity, float> enemies { get { return Enemies.dictionary; } }


	public class AreaCreate {
		public enum Area {
			left,
			right,
			up
		}
		public Area area;
		public AreaCreate(Area area) {
			this.area = area;
		}
		public AreaCreate(int i) {
			area = Area.up;
		}
		#region GETPOSITION
		public static Vector3 GetPosition(Area area) {
			Vector3 pos = new Vector3();
			Camera cam = Camera.main;
			float up = cam.ViewportToWorldPoint(new Vector3(0, 1)).y;
			float down = cam.ViewportToWorldPoint(new Vector3(0, 0)).y;
			float right = cam.ViewportToWorldPoint(new Vector3(1, 0)).x + PlayManager.borderField;
			float left = cam.ViewportToWorldPoint(new Vector3(0, 1)).x - PlayManager.borderField;
			switch (area) {
				case Area.left:
					pos.y = Random.Range(up, down);
					pos.x = left - 3;
					pos.z = 0;
					break;
				case Area.right:
					pos.y = Random.Range(up, down);
					pos.x = right + 3;
					pos.z = 0;
					break;
				case Area.up:
					pos.y = up + 3;
					pos.x = Random.Range(left, right);
					pos.z = 0;
					break;
			}
			return pos;
		}
		public Vector3 GetPosition() {
			Vector3 pos = new Vector3();
			Camera cam = Camera.main;
			float up = cam.ViewportToWorldPoint(new Vector3(0, 1)).y;
			float down = cam.ViewportToWorldPoint(new Vector3(0, 0)).y;
			float right = cam.ViewportToWorldPoint(new Vector3(1, 0)).x;
			float left = cam.ViewportToWorldPoint(new Vector3(0, 1)).x;
			switch (area) {
				case Area.left:
					pos.y = Random.Range(up, down);
					pos.x = left - 3;
					pos.z = 0;
					break;
				case Area.right:
					pos.y = Random.Range(up, down);
					pos.x = right + 3;
					pos.z = 0;
					break;
				case Area.up:
					pos.y = up + 3;
					pos.x = Random.Range(left, right);
					pos.z = 0;
					break;
			}
			return pos;
		}
		#endregion
		//Доделать направление под углом
		#region GETDIRECT 
		public static Vector3 GetDirect(Area area) {
			Vector3 direct = new Vector3();
			switch (area) {
				case Area.left:
					direct = Vector3.right;
					break;
				case Area.right:
					direct = Vector3.left;
					break;
				case Area.up:
					direct = Vector3.down;
					break;
			}
			return direct;
		}

		public Vector3 GetDirect() {
			Vector3 direct = new Vector3();
			switch (area) {
				case Area.left:
					direct = Vector3.right;
					break;
				case Area.right:
					direct = Vector3.left;
					break;
				case Area.up:
					direct = Vector3.down;
					break;
			}
			return direct;
		}
		#endregion

	}
	[SerializeField]
	public AreaCreate areaCreate;

	private void Start() {
		areaCreate = new AreaCreate(AreaCreate.Area.up);
		foreach(var i in enemies) {
			GameObject go = new GameObject();
			Generate g = go.AddComponent<Generate>();
			g.Init(i.Key, i.Value, areaCreate);

			Debug.Log(g.avengerTime);
		}
	}

	private void Update() {
		
	}


}