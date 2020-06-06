using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utilits
{
    
}
public static class ListUtilits {

	public static List<T> CopyTo<T>(List<T> list) {
		List<T> list2 = new List<T>();
		for (int i = 0; i < list.Count; i++) {
			list2.Add(list[i]);
		}
		return list2;
	}
}
