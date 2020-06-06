using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Skin : ScriptableObject
{
	public GameObject prefab;
	public Sprite iconHas;
	public Sprite iconNoHas;
	public int pack;
	public int index;
	public int has;
	public int globalID;
}
