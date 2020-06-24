using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
	public List<Entity> _enemies;
	public static List<Entity> enemies;
	private void Awake() {
		enemies = _enemies;
	}
}
